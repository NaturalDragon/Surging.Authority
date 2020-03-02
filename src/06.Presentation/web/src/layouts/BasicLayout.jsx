/**
 * Ant Design Pro v4 use `@ant-design/pro-layout` to handle Layout.
 * You can view component api by:
 * https://github.com/ant-design/ant-design-pro-layout
 */
import ProLayout, { DefaultFooter } from '@ant-design/pro-layout';
import React, { useEffect, Fragment } from 'react';
import Link from 'umi/link';
import { connect } from 'dva';
import { Icon, Result, Button } from 'antd';
import { formatMessage } from 'umi-plugin-react/locale';
import Authorized from '@/utils/Authorized';
import RightContent from '@/components/GlobalHeader/RightContent';
import { isAntDesignPro, getAuthorityFromRouter } from '@/utils/utils';
import { menuKey } from '@/utils/config'
import logo from '../assets/logo.png';

import './BasicLayout.less';
import '@/pages/global.less'

const noMatch = (
  <Result
    status="403"
    title="403"
    subTitle="Sorry, you are not authorized to access this page."
    extra={
      <Button type="primary">
        <Link to="/user/login">Go Login</Link>
      </Button>
    }
  />
);

/**
 * use Authorized check all menu item
 */
const menuDataRender = menuList => {
  let m = localStorage.getItem(menuKey);
  return JSON.parse(m);
}

// menuList.map(item => {
//   const localItem = { ...item, children: item.children ? menuDataRender(item.children) : [] };
//   return Authorized.check(item.authority, localItem, null);
// });

const menuData = [
  {
    path: '/',
    redirect: '/module',
  },

  {
    name: '模块设置',
    icon: 'setting',
    path: '/module',
    component: '/module',
  },
  {
    name: '菜单管理',
    icon: 'setting',
    path: '/menu',
    component: '/menu',
  },
  {
    name: '组织架构',
    icon: 'setting',
    path: '/structure',
    component: '/structure',
  },
  {
    name: '功能权限',
    icon: 'setting',
    path: '/power',
    component: '/power',
  },
  {
    component: './404',
  },
]
const defaultFooterDom = (
  <DefaultFooter

    copyright="2019 思源时代研发中心"
    links={[

    ]}
  />
);

const FooterRender = ({ collapsed }) => {
  // if (!isAntDesignPro()) {
  //   return defaultFooterDom;
  // }
  let collapseWidth = collapsed ? 80 : 256;
  return (
    <Fragment>
      {/* {defaultFooterDom} */}
      <div
        style={{
          position: 'fixed',
          textAlign: 'center',
          bottom: 0,
          left: `${collapseWidth}px`,
          height: 50,
          lineHeight: '50px',
          background: '#f0f2f5',
          width: `calc(100% - ${collapseWidth}px)`,
        }} >
        Copyright © 2019 思源时代研发中心
      </div>
    </Fragment>
  );
};

const BasicLayout = props => {
  const {
    collapsed,
    dispatch,
    children,
    settings,
    location = {
      pathname: '/',
    },
  } = props;
  /**
   * constructor
   */

  useEffect(() => {
    if (dispatch) {
      dispatch({
        type: 'user/fetchCurrent',
      });
    }
  }, []);
  /**
   * init variables
   */

  const handleMenuCollapse = payload => {
    if (dispatch) {
      dispatch({
        type: 'global/changeLayoutCollapsed',
        payload,
      });
    }
  }; // get children authority

  const authorized = getAuthorityFromRouter(props.route.routes, location.pathname || '/') || {
    authority: undefined,
  };
  return (
    <ProLayout
      logo={logo}
      menuHeaderRender={(logoDom, titleDom) => (
        <Link to="/">
          {logoDom}
          {titleDom}
        </Link>
      )}
      onCollapse={handleMenuCollapse}
      menuItemRender={(menuItemProps, defaultDom) => {
        if (menuItemProps.isUrl || menuItemProps.children) {
          return defaultDom;
        }

        return <Link to={menuItemProps.path}>{defaultDom}</Link>;
      }}
      breadcrumbRender={(routers = []) => [
        {
          path: '/',
          breadcrumbName: formatMessage({
            id: 'menu.home',
            defaultMessage: 'Home',
          }),
        },
        ...routers,
      ]}
      itemRender={(route, params, routes, paths) => {
        const first = routes.indexOf(route) === 0;
        return first ? (
          <Link to={paths.join('/')}>{route.breadcrumbName}</Link>
        ) : (
            <span>{route.breadcrumbName}</span>
          );
      }}
      footerRender={() => <FooterRender collapsed={collapsed} />}
      // menuDataRender={() => menuData}
      menuDataRender={menuDataRender}
      formatMessage={formatMessage}
      rightContentRender={rightProps => <RightContent {...rightProps} />}
      {...props}
      {...settings}
    >
      <Authorized authority={authorized.authority} noMatch={noMatch}>
        {children}
      </Authorized>
    </ProLayout>
  );
};

export default connect(({ global, settings }) => ({
  collapsed: global.collapsed,
  settings,
}))(BasicLayout);
