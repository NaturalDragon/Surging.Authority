import { DefaultFooter, getMenuData, getPageTitle } from '@ant-design/pro-layout';
import { Helmet } from 'react-helmet';
import Link from 'umi/link';
import React, { Fragment } from 'react';
import { connect } from 'dva';
import { formatMessage } from 'umi-plugin-react/locale';
import SelectLang from '@/components/SelectLang';
import logo from '../assets/logo.png';
import styles from './UserLayout.less';

const UserLayout = props => {
  debugger
  const {
    route = {
      routes: [],
    },
  } = props;
  const { routes = [] } = route;
  const {
    children,
    location = {
      pathname: '',
    },
  } = props;
  const { breadcrumb } = getMenuData(routes);
  const title = getPageTitle({
    pathname: location.pathname,
    breadcrumb,
    formatMessage,
    ...props,
  });
  return (
    <Fragment>
      <div className={styles.container}>
        <div className={styles.content} style={{ marginTop: 135 }}>
          <div className={styles.top}>
            <div className={styles.header}>
              <Link to="/">
                <img alt="logo" className={styles.logo} src={logo} />
                <span className={styles.title}>权限管理平台</span>
              </Link>
            </div>
          </div>
          {children}
        </div>
      </div>
    </Fragment>
  );
};

export default connect(({ settings }) => ({ ...settings }))(UserLayout);
