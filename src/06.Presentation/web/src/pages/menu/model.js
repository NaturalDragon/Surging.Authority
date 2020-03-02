import { message } from 'antd'
import { New, GetListPaged, Remove, Modify, GetForModfiy, GetMenuTree } from './service';
import { GetAll } from './../module/service'
import { toAntPagination, Guid } from '../../utils/com'
const guidEmpty = '00000000-0000-0000-0000-000000000000';
const Model = {
  namespace: 'menuModel',
  state: {
    list: [],
    moduleList: [],
    menuTreeList: [],
    pagination: {},
    dataInfo: {
      name: '',
      parentId: '',
      icon:'',
      sortIndex:0,
      moduleId: ''
    },
  },
  effects: {
    *fetch({ payload }, { call, put }) {
      const data = yield call(GetMenuTree, payload);
      if (data) {
        yield put({
          type: 'save',
          payload: {
            list: data,

          },
        });
      }
    },

    *getMenuTree({ payload }, { call, put }) {
      const data = yield call(GetMenuTree, payload);
      if (data) {
        data.unshift({ key: Guid(), value: guidEmpty, title: '无父级' })
        yield put({
          type: 'save',
          payload: {
            menuTreeList: data,
          },
        });
      }
    },
    *getAllModule({ payload }, { call, put }) {
      const data = yield call(GetAll, payload);
      if (data) {
        yield put({
          type: 'save',
          payload: {
            moduleList: data,
          },
        });
      }
    },
    *getForAdd({ payload, callback }, { call, put }) {
   
      yield put({ type: 'clear' })
      const [menuData, moduleData] = yield [
        call(GetMenuTree, {}),
        call(GetAll, {})
      ]
      if (menuData && moduleData) {
        menuData.unshift({ key: Guid(), value: guidEmpty, title: '无父级' })
        yield put({
          type: 'save',
          payload: {
            menuTreeList: menuData,
            moduleList: moduleData,
          },
        });
        if (callback) callback();
      }
    },
    *add({ payload, callback }, { call, put }) {
      const response = yield call(New, payload);
      if (response && response.isValid) {
        yield put({ type: 'clear' })
        if (callback) callback();
      } else {
        message.error(response.errorMessages, 2)
      }
    },

    *remove({ payload, callback }, { call, put }) {
      const response = yield call(Remove, payload);
      yield put({
        type: 'save',
        payload: response,
      });
      if (callback) callback();
    },
    *getForModfiy({ payload, callback }, { call, put }) {
      const [dataInfo, menuData, moduleData] = yield [
        call(GetForModfiy, payload),
        call(GetMenuTree, {menuId:payload.entityId}),
        call(GetAll, {})]
      if (dataInfo && menuData && moduleData) {
        menuData.unshift({ key: Guid(), value: guidEmpty, title: '无父级' })
        yield put({
          type: 'save',
          payload: {
            dataInfo,
            menuTreeList: menuData,
            moduleList: moduleData,
          },
        });
        if (callback) callback();
      } else {
        message.error(dataInfo.errorMessages, 2)
      }
    },
    *modify({ payload, callback }, { call, put }) {
      const response = yield call(Modify, payload);
      if (response && response.isValid) {
        yield put({ type: 'clear' })
        if (callback) callback();
      } else {
        message.error(response.errorMessages, 2)
      }
    },

  },
  reducers: {
    clear(state, action) {
      const { dataInfo } = state;
      
      // eslint-disable-next-line no-restricted-syntax
      // eslint-disable-next-line guard-for-in
      // eslint-disable-next-line no-restricted-syntax
      // eslint-disable-next-line guard-for-in
      for (const key in dataInfo) {
        if (key === 'id') { dataInfo[key] = Guid() }
        else{
          dataInfo[key] = '';}
      }
      // eslint-disable-next-line no-undef
      return { ...state, dataInfo }
    },
    save(state, action) {
      return { ...state, ...action.payload };
    },
  },
};
export default Model;
