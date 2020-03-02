import request from '@/utils/request';

export async function fakeAccountLogin(params) {
  return request(`/api/Users/Authentication`, {
    method: 'POST',
    body: JSON.stringify(params),
  });
}
export async function getFakeCaptcha(mobile) {
  return request(`/api/login/captcha?mobile=${mobile}`);
}

export async function GetCurrentMenuTree(params) {
  return request(`/api/Menu/GetCurrentMenuTree`, {
    method: 'POST',
    body: JSON.stringify(params),
  });

}