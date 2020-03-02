import request from '@/utils/request';


export async function New(params) {
    return request('/api/Organization/Create', {
        method: 'POST',
        body: JSON.stringify(params),
    })
}
export async function GetForModify(params) {
    return request('/api/Organization/GetForModify', {
        method: 'POST',
        body: JSON.stringify(params),
    })
}
export async function Modify(params) {
    return request('/api/Organization/Modify', {
        method: 'POST',
        body: JSON.stringify(params),
    })
}
export async function Remove(params) {
    return request('/api/Organization/Remove', {
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
