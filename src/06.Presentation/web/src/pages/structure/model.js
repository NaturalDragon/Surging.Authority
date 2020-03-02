/* eslint-disable no-restricted-syntax */
import { message } from 'antd'
import { New, GetListPaged, Remove, Modify, GetForModfiy } from './service';
import { toAntPagination } from '../../utils/com'

const Model = {
  namespace: 'structureModel',
  state: {
    list: [],
    pagination: {},
    orgList:[],
    roleList:[],
    dataInfo: {
      name: '',
      gender:0
    },
  },
  effects: {
    *fetch({ payload }, { call, put }) {
      const data = yield call(GetListPaged, payload);
      if (data) {
        yield put({
          type: 'save',
          payload: {
            list: data.data,
            pagination: toAntPagination(data),
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
            orgList:response.organizationObjects,
            roleList:response.roleObjects
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
      return { ...state, dataInfo,orgList:[] }
    },
    save(state, action) {
      return { ...state, ...action.payload };
    },
  },
};
export default Model;
