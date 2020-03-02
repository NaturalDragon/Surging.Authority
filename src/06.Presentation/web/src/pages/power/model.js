/* eslint-disable no-restricted-syntax */
import { message } from 'antd'
import _ from 'underscore';
import {
  New, GetListPaged, Remove, Modify, GetForModfiy, GetAllModule, GetMenuTree, GetOrgMenu, SaveOrgMenu,
  GetRoleMenu, SaveRoleMenu, GetOrganizationModuleElement,GetRoleModuleElement,GetEmployeeModuleElement,GetEmployeeElementPower, SaveOrganizationModuleElement
  ,SaveEmployeeModuleElement,SaveRoleModuleElement, GetEmployeeMenu, SaveEmployeeMenu
} from './service';
import { authorAction } from '@/utils/constant'
import { toAntPagination } from '@/utils/com'

function MenuItemToDisable(menuList, disIds) {
  if (menuList instanceof Array) {
    menuList.forEach(menu => {
      if (disIds.includes(menu.key)) {
        menu.disabled = true;
      } else {
        menu.disabled = false;
      }
      if (menu.children instanceof Array && menu.children.length > 0) {
        MenuItemToDisable(menu.children, disIds)
      }
    })
  }
}
const Model = {
  namespace: 'powerModel',
  state: {
    list: [],
    pagination: {},
    orgList: [],
    roleList: [],
    moduleList: [],
    moduleCheckIds: [],
    menuCheckIds: [],
    menuList: [],
    powerMemuLoading: false,
    powerMenuActing: false,
    powerElementLoading: false,
    powerElementActing: false,
    dataInfo: {
      name: '',
      gender: 0
    },
  },
  effects: {
    *getAllModule({ payload }, { call, put }) {
      const data = yield call(GetAllModule, payload);
      debugger
      if (data) {
        data.forEach(mod => {
          mod.checkedValues = [];
          mod.moduleElementActionRequests.forEach(ele => {
            ele.label = ele.name;
            ele.value = ele.id
          })
        })
        yield put({
          type: 'save',
          payload: {
            moduleList: data
          }
        })
      }
    },
    *getElementPower({ payload,currentItem }, { call, put, select }) {
      yield put({
        type: 'save',
        payload: { powerElementLoading: true }
      })
      let data;
      if(currentItem.action === authorAction.organization){
         data = yield call(GetOrganizationModuleElement, payload);
      }else if(currentItem.action === authorAction.role){
         data = yield call(GetRoleModuleElement, payload);
      }
     
      
      if (data) {
        const { powerModel } = yield select((powerModel) => { return powerModel })
        const checkedIds=_.pluck(data,'id');
        const disabledList=_.where(data,{disabled:true});
        const disabledIds=_.pluck(disabledList,'id');

        const newModuleList = powerModel.moduleList;
        newModuleList.forEach(mod => {
          const itemCheckedValues = []
          mod.moduleElementActionRequests.forEach(ele => {
            //  ele.disabled=true;
              if(disabledIds.includes(ele.id)){
                ele.disabled=true;
              }else{
                ele.disabled=false;
              }

            if (checkedIds.includes(ele.id)) {
              itemCheckedValues.push(ele.id);
            }
          })
          // eslint-disable-next-line no-param-reassign
          mod.checkedValues = itemCheckedValues;
        })
        yield put({
          type: 'save',
          payload: {
            moduleCheckIds: data,
            powerElementLoading: false,
            moduleList: newModuleList
          }
        })
      }
    },

    *getEmployeeElementPower({ payload }, { call, put, select }) {
      yield put({
        type: 'save',
        payload: { powerElementLoading: true }
      })
      const data = yield call(GetEmployeeElementPower, payload);
      if (data) {
        const { powerModel } = yield select((powerModel) => { return powerModel })

        const checkedIds=_.pluck(data,'id');
        const disabledList=_.where(data,{disabled:true});
        const disabledIds=_.pluck(disabledList,'id');

        const newModuleList = powerModel.moduleList;
        newModuleList.forEach(mod => {
          const itemCheckedValues = []
          mod.moduleElementActionRequests.forEach(ele => {
            //  ele.disabled=true;
              if(disabledIds.includes(ele.id)){
                ele.disabled=true;
              }else{
                ele.disabled=false;
              }

            if (checkedIds.includes(ele.id)) {
              itemCheckedValues.push(ele.id);
            }
          })
          // eslint-disable-next-line no-param-reassign
          mod.checkedValues = itemCheckedValues;
        })
        yield put({
          type: 'save',
          payload: {
            moduleCheckIds: data,
            powerElementLoading: false,
            moduleList: newModuleList
          }
        })
      }
    },
    *SaveOrganizationModuleElement({ payload, callBack }, { call, put }) {

      const data = yield call(SaveOrganizationModuleElement, payload);
      if (data) {
        if (data.isValid) {
          message.success('保存成功')
          if (callBack) callBack()
        } else {
          message.error(data.errorMessages)
        }
      }
    },
    *SaveRoleModuleElement({ payload, callBack }, { call, put }) {

      const data = yield call(SaveRoleModuleElement, payload);
      if (data) {
        if (data.isValid) {
          message.success('保存成功')
          if (callBack) callBack()
        } else {
          message.error(data.errorMessages)
        }
      }
    },
    *SaveEmployeeModuleElement({ payload, callBack }, { call, put }) {

      const data = yield call(SaveEmployeeModuleElement, payload);
      if (data) {
        if (data.isValid) {
          message.success('保存成功')
          if (callBack) callBack()
        } else {
          message.error(data.errorMessages)
        }
      }
    },
    *getOrgMenu({ payload }, { call, put }) {

      const data = yield call(GetOrgMenu, payload);
      if (data) {
        yield put({
          type:'saveMapMenu',
          payload:{
            data
          }
        })
        // yield put({
        //   type: 'save',
        //   payload: {
        //     menuCheckIds: data,
        //     powerMemuLoading: false
        //   }
        // })
      }
    },
    *saveOrgMenu({ payload, callBack }, { call, put }) {
      const data = yield call(SaveOrgMenu, payload);
      if (data.isValid) {
        message.success('保存成功')
        if (callBack) callBack()
      } else {
        message.error(data.errorMessages)
      }
    },

    *getRoleMenu({ payload }, { call, put }) {
      const data = yield call(GetRoleMenu, payload);
      if (data) {
        yield put({
          type:'saveMapMenu',
          payload:{
            data
          }
        })
        // yield put({
        //   type: 'save',
        //   payload: {
        //     menuCheckIds: data,
        //     powerMemuLoading: false
        //   }
        // })
      }
    },
    *saveRoleMenu({ payload, callBack }, { call, put }) {
      const data = yield call(SaveRoleMenu, payload);
      if (data.isValid) {
        message.success('保存成功')
        if (callBack) callBack()
      } else {
        message.error(data.errorMessages)
      }
    },

    *getEmployeeMenu({ payload }, { call, put, select }) {
      const data = yield call(GetEmployeeMenu, payload);
      if (data) {

        yield put({
          type:'saveMapMenu',
          payload:{
            data
          }
        })
        // const { powerModel } = yield select((powerModel) => { return powerModel })
        // debugger
        // let { menuList } = powerModel;
        // let disabledList = _.where(data, { disabled: true })
        // let checkedIds = _.pluck(data, 'id');
        // let disabledIds = _.pluck(disabledList, 'id')

        // MenuItemToDisable(menuList, disabledIds)
        // debugger
        // yield put({
        //   type: 'save',
        //   payload: {
        //     menuCheckIds: checkedIds,//data,
        //     menuList: menuList,
        //     powerMemuLoading: false
        //   }
        // })
      }
    },
    *saveEmployeeMenu({ payload, callBack }, { call, put }) {
      const data = yield call(SaveEmployeeMenu, payload);
      if (data.isValid) {
        message.success('保存成功')
        if (callBack) callBack()
      } else {
        message.error(data.errorMessages)
      }
    },

    *getMenuTree({ payload }, { call, put }) {
      const data = yield call(GetMenuTree, payload);
      if (data) {
        yield put({
          type: 'save',
          payload: {
            menuList: data
          }
        })
      }
    },
    *fetch({ payload }, { call, put }) {
      const data = yield call(GetListPaged, payload);
      if (data) {
        yield put({
          type: 'save',
          payload: {
            list: data.employeeList,
            pagination: toAntPagination(data.pagination),
          },
        });
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
      const response = yield call(GetForModfiy, payload);
      if (response) {
        yield put({
          type: 'save',
          payload: {
            dataInfo: response,
            orgList: response.organizationObjects,
            roleList: response.roleObjects
          },
        });
        if (callback) callback();
      } else {
        message.error(response.errorMessages, 2)
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

      for (const key in dataInfo) {
        dataInfo[key] = '';
      }
      return { ...state, dataInfo, orgList: [] }
    },
    saveMapMenu(state, action) {
      const {data} = action.payload
      const { menuList } = state;
      const disabledList = _.where(data, { disabled: true })
      const checkedIds = _.pluck(data, 'id');
      const disabledIds = _.pluck(disabledList, 'id')
      MenuItemToDisable(menuList, disabledIds)
      
      return {
        ...state,
        menuCheckIds: checkedIds,//data,
        menuList,
        powerMemuLoading: false
      }

    },

    save(state, action) {
      return { ...state, ...action.payload };
    },
  },
};
export default Model;
