import { message } from 'antd'
import { Create, GetListPaged, Remove, Modify, GetForModfiy } from './service';
import { toAntPagination, Guid } from '../../utils/com'

const Model = {
  namespace: 'moduleModel',
  state: {
    list: [],
    pagination: {},
    dataInfn: {
      id: Guid(),
      name: '',
      url: '',
      isEnabled: true,
      operationStatus: 1,
      moduleElementActionRequests: [
        // {id:Guid(), name: '新增', routePath: '/api/test/new' },
        // {id:Guid(), name: '修改', routePath: '/api/test/modify' },
        // {id:Guid(), name: '删除', routePath: '/api/test/remove' }
      ]
    },
  },
  effects: {
    *fetch({ payload }, { call, put }) {
      const data = yield call(GetListPaged, payload);
      debugger
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

    *create({ payload, callback }, { call, put }) {

      const response = yield call(Create, payload);
      debugger
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
            dataInfn: response
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
      const { dataInfn } = state;
      // eslint-disable-next-line no-restricted-syntax
      // eslint-disable-next-line guard-for-in
      // eslint-disable-next-line no-restricted-syntax
      // eslint-disable-next-line guard-for-in
      for (const key in dataInfn) {
        if (key === 'id') { dataInfn[key] = Guid() }
        else if (key === 'operationStatus') { dataInfn[key] = 1 }
        else if (key === 'isEnabled') { dataInfn[key] = true }
        else if (dataInfn[key] instanceof Array) {
          dataInfn[key] = [];
        } else if ((typeof dataInfn[key]) === Boolean) {
          dataInfn[key] = true;
        } else {
          dataInfn[key] = '';
        }

      }
      debugger
      // eslint-disable-next-line no-undef
      return { ...state, dataInfn }
    },
    save(state, action) {
      return { ...state, ...action.payload };
    },
  },
};
export default Model;
