// /**
//  * request 网络请求工具
//  * 更详细的 api 文档: https://github.com/umijs/umi-request
//  */
// import request,{extend } from 'umi-request';
// import { notification,  message} from 'antd';
// import { tokenKey } from '@/utils/config'
// import { GetCookie } from '@/utils/com'

// const codeMessage = {
//   200: '服务器成功返回请求的数据。',
//   201: '新建或修改数据成功。',
//   202: '一个请求已经进入后台排队（异步任务）。',
//   204: '删除数据成功。',
//   400: '发出的请求有错误，服务器没有进行新建或修改数据的操作。',
//   401: '用户没有权限（令牌、用户名、密码错误）。',
//   403: '用户得到授权，但是访问是被禁止的。',
//   404: '发出的请求针对的是不存在的记录，服务器没有进行操作。',
//   406: '请求的格式不可得。',
//   410: '请求的资源被永久删除，且不会再得到的。',
//   422: '当创建一个对象时，发生一个验证错误。',
//   500: '服务器发生错误，请检查服务器。',
//   502: '网关错误。',
//   503: '服务不可用，服务器暂时过载或维护。',
//   504: '网关超时。',
// };
// /**
//  * 异常处理程序
//  */

// const errorHandler = error => {
//   const { response } = error;

//   if (response && response.status) {
//     const errorText = codeMessage[response.status] || response.statusText;
//     const { status, url } = response;
//     notification.error({
//       message: `请求错误 ${status}: ${url}`,
//       description: errorText,
//     });
//   } else if (!response) {
//     notification.error({
//       description: '您的网络发生异常，无法连接服务器',
//       message: '网络异常',
//     });
//   }

//   return response;
// };


// function toCamel(o) {
//   if (typeof (o) == 'string') {
//     o = JSON.parse(o)
//   }
//   var newO, origKey, newKey, value
//   if (o instanceof Array) {
//     newO = []
//     for (origKey in o) {
//       value = o[origKey]
//       if (typeof value === 'object') {
//         value = toCamel(value)
//       }
//       newO.push(value)
//     }
//   } else {
//     newO = {}
//     for (origKey in o) {
//       if (o.hasOwnProperty(origKey)) {
//         newKey = (origKey.charAt(0).toLowerCase() + origKey.slice(1) || origKey).toString()
//         value = o[origKey]
//         if (value !== null && (value.constructor === Object || value instanceof Array)) {
//           value = toCamel(value)
//         }
//         newO[newKey] = value
//       }
//     }
//   }
//   return newO
// }

// async function requestX  (url, options) {
//   const token = localStorage.getItem(tokenKey);
//   let authorization;
//   if (token) {
//     authorization = `Bearer ${token}`
//   }
//   if (options.headers) {
//     options.headers['Accept'] = 'application/json,text/plain,*/*';
//     options.headers['Content-Type'] = 'application/json; charset=utf-8';
//     options.headers['Authorization'] = authorization;
//   }
//   else {
//     options.headers = {
//       'Accept': 'application/json,text/plain,*/*',
//       "Content-Type": "application/json; charset=utf-8",
//       Authorization: authorization
//     }
//   }
//   let httpUrl;
//   if (url.indexOf('http') > -1 || url.indexOf('http') > -1) {
//     httpUrl = url;
//   } else {
//     httpUrl =config.serverIp + url;
//   }
//   if(options.body){
//     const bodyJson=JSON.parse(options.body);
//     options.body=JSON.stringify({input:bodyJson})

//   }
//   const optionsre = { ...options, ...{ errorHandler: errorHandler } }
//   request.interceptors.response.use( async (response) => {
//     const json = await response.clone().json();
//     debugger
//     if (json.hasOwnProperty('entity')) {
//       debugger
//       if (json.entity instanceof Object) {
//         json.entity = toCamel(json.entity);
//         if (json.isSucceed===false) {

//           notification.error({
//             message: `请求错误 `,
//             description: json.message,
//           });

//         }
//         debugger
//         return json.entity;
//       }
//       if (json.entity === 'null') {
//         return null;
//       }
//     }
//     debugger
//     return json;
//   })
//   const result=  await request(httpUrl,optionsre)
//   return result;

// }

// export default requestX;



import fetch from 'dva/fetch';
import { notification, message } from 'antd';
import { tokenKey } from '@/utils/config'
import { GetCookie } from '@/utils/com'

function toCamel(o) {
  if (typeof (o) == 'string') {
    o = JSON.parse(o)
  }
  var newO, origKey, newKey, value
  if (o instanceof Array) {
    newO = []
    for (origKey in o) {
      value = o[origKey]
      if (typeof value === 'object') {
        value = toCamel(value)
      }
      newO.push(value)
    }
  } else {
    newO = {}
    for (origKey in o) {
      if (o.hasOwnProperty(origKey)) {
        newKey = (origKey.charAt(0).toLowerCase() + origKey.slice(1) || origKey).toString()
        value = o[origKey]
        if (value !== null && (value.constructor === Object || value instanceof Array)) {
          value = toCamel(value)
        }
        newO[newKey] = value
      }
    }
  }
  return newO
}
function parseText(response) {
  return response.text();
}
function parseJSON(text) {
  debugger
  try {
    var json = JSON.parse(text);

    if (json.isSucceed) {
      if (json.hasOwnProperty('entity')) {

        if (json.entity instanceof Object) {
          json.entity = toCamel(json.entity);
          if (json.isSucceed === false) {
            notification.error({
              message: `请求错误 `,
              description: json.message,
            });
            // message.warn(json.errorMessages || json.errorMessages, 2, () => { message.destroy() })
          }
          return json.entity;
        }
        if (json.entity === 'null') {
          return null;
        }
      }
    }else{
      notification.error({
        message: `请求错误 `,
        description: json.message,
      }); 
    }

    return json;
  }
  catch (e) {
    return text;
  }
}

function checkStatus(response) {
  if (response.status >= 200 && response.status < 300) {
    return response;
  }

  const error = new Error(response.statusText);
  error.response = response;
  throw error;
}

/**
 * Requests a URL, returning a promise.
 *
 * @param  {string} url       The URL we want to request
 * @param  {object} [options] The options we want to pass to "fetch"
 * @return {object}           An object containing either "data" or "err"
 */
export default function request(url, options) {
  const token = localStorage.getItem(tokenKey);
  let authorization;
  if (token) {
    authorization = `Bearer ${token}`
  }
  if (options.headers) {
    options.headers['Accept'] = 'application/json,text/plain,*/*';
    options.headers['Content-Type'] = 'application/json; charset=utf-8';
    options.headers['Authorization'] = authorization;
  }
  else {
    options.headers = {
      'Accept': 'application/json,text/plain,*/*',
      "Content-Type": "application/json; charset=utf-8",
      Authorization: authorization
    }
  }
  if (options.body) {
    const bodyJson = JSON.parse(options.body);
    options.body = JSON.stringify({ input: bodyJson })

  }

  var httpUrl;

  if (url.indexOf('http') > -1 || url.indexOf('http') > -1) {
    httpUrl = url;
  } else {
    httpUrl = config.serverIp + url;
  }
  return fetch(`${httpUrl}`, options)
    .then(checkStatus)
    .then(parseText)
    .then(parseJSON)
    //.then(data => ({ data }))
    .catch(err => ({ err }));
}
