import request from '@/utils/request';

export async function GetListPaged(params) {
  return request('/api/Employee/GetListPagedByOrgIdOrRoleId', {
    method: 'POST',
    body: JSON.stringify(params),
  });
}
export async function Remove(params) {
  return request('/api/Employee/Remove', {
    method: 'POST',
    body: JSON.stringify(params),
  });
}
export async function New(params) {
  return request('/api/Employee/Create', {
    method: 'POST',
    body: JSON.stringify(params),
  });
}
export async function Modify(params) {
  return request('/api/Employee/Modify', {
    method: 'POST',
    body: JSON.stringify(params),
  });
}

export async function GetForModfiy(params) {
  return request('/api/Employee/GetForModify', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}
export async function GetOrganizationByParentId(params) {
  return request('/api/Organization/GetOrganizationByParentId', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}

