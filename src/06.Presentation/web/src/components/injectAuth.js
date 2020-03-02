/* eslint-disable react/sort-comp */
/* eslint-disable import/no-mutable-exports */

import { Component } from 'react'
import { routePathsKey } from '@/utils/config'
import { platIncludes, platIncludesArray } from '@/utils/com'

export default function injectAuthFuc(ComposedComponent) {

    const checkAuth = (props) => {
        const { auth } = props;
        // No Auth required
        if (auth == null) {
            return true;
        }
        // 如果是path集合，则任意一个path存在都通过权限验证
        // eslint-disable-next-line no-empty
        const routePaths = localStorage.getItem(routePathsKey);
        const routePathsArray = JSON.parse(routePaths);
        if (auth instanceof Array) {
            return platIncludesArray(routePathsArray, auth);
        } 
            return platIncludes(routePathsArray, auth);
        
        // return windows.globals.permissions[auth];
    }

    // eslint-disable-next-line react/prefer-stateless-function
    const WrapClass = class extends Component {
        // 构造
        // eslint-disable-next-line no-useless-constructor
        constructor(props) {
            super(props);
        }

        render() {
             if (checkAuth(this.props)) {
                // if(true){
                // eslint-disable-next-line react/react-in-jsx-scope
                return <ComposedComponent  {...this.props} />;
            }
            return null;
        }
    }
    return WrapClass;



};