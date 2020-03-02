import request from '@/utils/request';

export async function GetListPaged(params) {
  return request('/api/module/GetPageList', {
    method: 'POST',
    body: JSON.stringify(params),
  });
}
export async function Remove(params) {
  return request('/api/module/Remove', {
    method: 'POST',
    body: JSON.stringify(params),
  });
}
export async function Create(params) {
  return request('/api/module/Create', {
    method: 'POST',
    body: JSON.stringify(params),
  });
}
export async function Modify(params) {
  return request('/api/module/Modify', {
    method: 'POST',
    body: JSON.stringify(params),
  });
}

export async function GetForModfiy(params) {
  return request('/api/module/GetForModify', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}
export async function GetAll(params) {
  return request('/api/module/GetAll', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}
