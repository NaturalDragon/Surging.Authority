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
  return request('/api/Employee/New', {
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
  return request('/api/Organization/GetOrganizationAndEmployeeLower', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}

export async function GetAllModule(params) {
  return request('/api/Module/GetModulesWithElements', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}

export async function GetMenuTree(params) {
  return request('/api/Menu/GetMenuTree', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}

export async function GetOrgMenu(params) {
  return request('/api/Menu/GetOrgMenu', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}


export async function SaveOrgMenu(params) {
  return request('/api/Menu/SaveOrgMenu', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}

export async function GetRoleMenu(params) {
  return request('/api/Menu/GetRoleMenu', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}


export async function SaveRoleMenu(params) {
  return request('/api/Menu/SaveRoleMenu', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}



export async function GetEmployeeMenu(params) {
  return request('/api/Menu/GetEmployeeMenu', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}


export async function SaveEmployeeMenu(params) {
  return request('/api/Menu/SaveEmployeeMenu', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}



export async function GetOrganizationModuleElement(params) {
  return request('/api/ModuleElement/GetOrganizationModuleElement', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}
export async function GetRoleModuleElement(params) {
  return request('/api/ModuleElement/GetRoleModuleElement', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}
export async function GetEmployeeModuleElement(params) {
  return request('/api/ModuleElement/GetEmployeeModuleElement', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}


export async function GetEmployeeElementPower(params) {
  return request('/api/ModuleElement/GetEmployeeModuleElement', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}


export async function SaveOrganizationModuleElement(params) {
  return request('/api/ModuleElement/SaveOrganizationModuleElement', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}
export async function SaveEmployeeModuleElement(params) {
  return request('/api/ModuleElement/SaveEmployeeModuleElement', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}
export async function SaveRoleModuleElement(params) {
  return request('/api/ModuleElement/SaveRoleModuleElement', {
    method: 'POST',
    body: JSON.stringify(params),
  })
}


