import request from '@/utils/request';

export async function GetListPaged(params) {
  return request('/api/menu/GetListPaged', {
    method: 'POST',
    body: JSON.stringify(params),
  });
}
export async function Remove(params) {
  return request('/api/menu/Remove', {
    method: 'POST',
    body: JSON.stringify(params),
  });
}
export async function New(params) {
  return request('/api/menu/Create', {
    method: 'POST',
    body: JSON.stringify(params),
  });
}
export async function Modify(params) {
  return request('/api/menu/Modify', {
    method: 'POST',
    body: JSON.stringify(params),
  });
}

export async function GetForModfiy(params) {
  return request('/api/menu/GetForModify', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}
export async function GetMenuTree(params) {
  return request('/api/menu/GetMenuTree', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}
