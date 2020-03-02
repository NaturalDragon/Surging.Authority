import request from '@/utils/request';


export async function GetListPagedOriginal(params) {
    return request('/api/Employee/GetListPagedOriginal', {
        method: 'POST',
        body: JSON.stringify(params),
    })
}

