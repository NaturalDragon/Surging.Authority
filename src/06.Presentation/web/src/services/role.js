import request from '@/utils/request';


export async function New(params) {
    return request('/api/role/Create', {
        method: 'POST',
        body: JSON.stringify(params),
    })
}
export async function GetForModify(params) {
    return request('/api/role/GetForModify', {
        method: 'POST',
        body: JSON.stringify(params),
    })
}
export async function Modify(params) {
    return request('/api/role/Modify', {
        method: 'POST',
        body: JSON.stringify(params),
    })
}

export async function Remove(params) {
    return request('/api/role/Remove', {
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
export async function GetRoleTypeList(params) {
    return request('/api/role/GetRoleTypeList', {
        method: 'POST',
        body: JSON.stringify(params),
    })
}

