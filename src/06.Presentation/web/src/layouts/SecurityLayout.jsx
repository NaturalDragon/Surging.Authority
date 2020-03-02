import React from 'react';
import { connect } from 'dva';
import { Redirect } from 'umi';
import { stringify } from 'querystring';
import PageLoading from '@/components/PageLoading';
import { tokenKey } from '@/utils/config'
import { GetCookie } from '@/utils/com'

class SecurityLayout extends React.Component {
  state = {
    isReady: false,
  };

  componentDidMount() {
    this.setState({
      isReady: true,
    });
    const { dispatch } = this.props;

    if (dispatch) {
      dispatch({
        type: 'user/fetchCurrent',
      });
    }
  }

  render() {
    const { isReady } = this.state;
    debugger
    const { children, loading, currentUser } = this.props; // You can replace it to your authentication rule (such as check token exists)
    // 你可以把它替换成你自己的登录认证规则（比如判断 token 是否存在）
    const token = localStorage.getItem(tokenKey);
    const isLogin = currentUser && currentUser.name;
    const queryString = stringify({
      redirect: window.location.href,
    });

    if ((!isLogin && loading) || !isReady) {
      return <PageLoading />;
    }

    if (!token) {
      return <Redirect to={`/user/login?${queryString}`}></Redirect>;
    }
    debugger
    return children;
  }
}

export default connect(({ user, loading }) => ({
  currentUser: user.currentUser,
  loading: loading.models.user,
}))(SecurityLayout);
