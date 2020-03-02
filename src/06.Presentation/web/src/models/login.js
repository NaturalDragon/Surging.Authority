import { routerRedux } from 'dva/router';
import { message } from 'antd';
import { stringify } from 'querystring';
import { fakeAccountLogin, getFakeCaptcha,GetCurrentMenuTree } from '@/services/login';
import { setAuthority } from '@/utils/authority';
import { getPageQuery } from '@/utils/utils';
import { tokenKey, userInfoKey,menuKey ,routePathsKey} from '@/utils/config'
import { SetCookie } from '@/utils/com'
import userlogo from '../assets/user.png'
const Model = {
  namespace: 'login',
  state: {
    status: undefined,
  },
  effects: {
    *login({ payload }, { call, put }) {
      const response = yield call(fakeAccountLogin, {...payload});
      debugger
      yield put({
        type: 'changeLoginStatus',
        payload: response,
      }); // Login successfully
      if (response.isSucceed) {
        
        localStorage.setItem(tokenKey, response.accessToken);
        localStorage.setItem(routePathsKey, JSON.stringify(response.routePaths))
        const info = JSON.stringify({
          name: payload.userId,
          avatar: userlogo,
        });
        localStorage.setItem(userInfoKey, info)
          // 获取菜单
         const menuData = yield call(GetCurrentMenuTree, {});
         if(menuData instanceof Array){
         localStorage.setItem(menuKey, JSON.stringify(menuData))
        }else{
          localStorage.setItem(menuKey, JSON.stringify([]))
        }
        // yield put({
        //   type: 'user/saveCurrentUser',
        //   payload: {
        //     name: payload.name,
        //     avatar: userlogo
        //   }
        // })
        const urlParams = new URL(window.location.href);
        const params = getPageQuery();
        let { redirect } = params;

        if (redirect) {
          const redirectUrlParams = new URL(redirect);

          if (redirectUrlParams.origin === urlParams.origin) {
            redirect = redirect.substr(urlParams.origin.length);

            if (redirect.match(/^\/.*#/)) {
              redirect = redirect.substr(redirect.indexOf('#') + 1);
            }
          } else {
            window.location.href = '/';
            return;
          }
        }

        yield put(routerRedux.replace(redirect || '/'));
      } else {
        message.error(response.errorMessages, 2);
      }
    },

    *getCaptcha({ payload }, { call }) {
      yield call(getFakeCaptcha, payload);
    },

    *logout(_, { put }) {
      const { redirect } = getPageQuery(); // redirect
      localStorage.clear()
      if (window.location.pathname !== '/user/login' && !redirect) {
        yield put(
          routerRedux.replace({
            pathname: '/user/login',
            search: stringify({
              redirect: window.location.href,
            }),
          }),
        );
      }
    },
  },
  reducers: {
    changeLoginStatus(state, { payload }) {
      // setAuthority(payload.currentAuthority);
      return { ...state, status: payload.status, type: payload.type };
    },
  },
};
export default Model;
