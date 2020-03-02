function SetCookie(name, value) {
    document.cookie = `${name}=${escape(value)};path=/`;
}
function GetCookie(objName) {
    const arrStr = document.cookie.split('; ');
    // eslint-disable-next-line vars-on-top
    // eslint-disable-next-line no-plusplus
    for (let i = 0; i < arrStr.length; i++) {
        // eslint-disable-next-line vars-on-top
        const temp = arrStr[i].split('=');
        if (temp[0] === objName) return unescape(temp[1]);
    }
    return '';
}
function GetPkey() {
    return GetCookie('UserID')
}

var GetQueryUrlString = function (url, name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var param = url.split('?')[1];
    if (!param) return null
    var r = param.match(reg);
    if (r != null) return r[2]; return null;
}

function Guid() {
    function S4() {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    }
    return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
}

function getChildCList(Component, list) {
    let l = list.filter(a => a.container === Component.id);
    let t = this;
    l.forEach(function (a) {
        if (a.isContainer) {
            let i = [];
            i = i.concat(t.getChildCList(a, list));
            l = l.concat(i);
        }
    });
    return l;
}
function getCList(container, list) {
    return list.filter(a => a.container === container);
}
function buildTableHeader(list, parent = '') {
    let t = this;
    let l = list.filter(a => a.parent === parent);
    l.forEach((e) => {
        let children = t.buildTableHeader(list, e.id);
        e.children = children;
        e.showChildren = true;
    });
    return l;
}
function ControlFormart(type) {
    var formart = null;
    switch (type) {
        case "none":
            break;
        case "Mobile":
            formart = /^1[3,4,5,8,7]\d{9}$/;
            break;
        case "IdCard":
            formart = /^\d{15}|\d{18}$","^\d{15}$|^\d{17}[\da-zA-z]$/;
            break;
        case "PostalCode":
            formart = /^[1-9][0-9]{5}$/;
            break;
        case "Email":
            formart = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
            break;
        default:
            break;
    }
    return formart;
}
function dateFormat(date, fmt) {
    if (typeof (date) === 'string')
        date = new Date(date);
    let o = {
        "M+": date.getMonth() + 1,               //月份   
        "d+": date.getDate(),                    //日   
        "h+": date.getHours(),                   //小时   
        "m+": date.getMinutes(),                 //分   
        "s+": date.getSeconds(),                 //秒   
        "q+": Math.floor((date.getMonth() + 3) / 3), //季度   
        "S": date.getMilliseconds()             //毫秒   
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length === 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

function StopEvent(e) {
    e.stopPropagation();
    e.nativeEvent.stopImmediatePropagation();
}
//对象里面提取部分属性PickAttr(obj, ['a', 'd', 'e'])
const PickAttr = (obj, arr) =>
    arr.reduce((iter, val) => (val in obj && (iter[val] = obj[val]), iter), {});


/**
     * [dateDiff 算时间差]
     * @param  {[type=Number]} hisTime [历史时间，必传]
     * @param  {[type=Number]} nowTime [当前时间，不传将获取当前时间戳]
     * @return {[string]}         [string]
     */
var dateDiff = function (hisTime, nowTime) {
    if (!arguments.length) return '';
    var arg = arguments,
        now = nowTime ? new Date(nowTime).getTime() : new Date().getTime(),
        diffValue = now - new Date(hisTime).getTime(),
        result = '',

        minute = 1000 * 60,
        hour = minute * 60,
        day = hour * 24,
        halfamonth = day * 15,
        month = day * 30,
        year = month * 12,

        _year = diffValue / year,
        _month = diffValue / month,
        _week = diffValue / (7 * day),
        _day = diffValue / day,
        _hour = diffValue / hour,
        _min = diffValue / minute;

    if (_year >= 1) result = parseInt(_year) + "年前";
    else if (_month >= 1) result = parseInt(_month) + "个月前";
    else if (_week >= 1) result = parseInt(_week) + "周前";
    else if (_day >= 1) result = parseInt(_day) + "天前";
    else if (_hour >= 1) result = parseInt(_hour) + "个小时前";
    else if (_min >= 1) result = parseInt(_min) + "分钟前";
    else result = "刚刚";
    return result;
}
const toAntPagination = (pagination) => {

    return {
        current: pagination.pageIndex,
        pageSize: pagination.pageSize,
        total: pagination.total
    }
}

const getValue = obj =>
    Object.keys(obj)
        .map(key => obj[key])
        .join(',');


function GetSubtract(dataA, dataB) {
    var newDataA = dataA.filter(ele => { return ele });
    var newDataB = dataB.filter(ele => { return ele });
    for (let i = newDataA.length - 1; i >= 0; i--) {
        for (let j = 0; j < newDataB.length; j++) {
            if (newDataA[i].id === newDataB[j].id) {
                newDataA.splice(i, 1);
                break;
            }
        }
    }
    return newDataA;
}

function LoadTree(treeData, parentId, nowTree) {

    treeData.forEach(ele => {
        if (ele.id === parentId) {
            ele.children = nowTree;
        }
        if (ele.children) {
            return LoadTree(ele.children, parentId, nowTree);
        }
    })
    return treeData;
}
// eslint-disable-next-line consistent-return
const platIncludes = (arr, path) => {
    if (arr instanceof Array) {
        let result = false;
        // eslint-disable-next-line no-plusplus
        for (let i = 0; i < arr.length; i++) {
            const ele = arr[i];
            if (ele.toLowerCase() === path.toLowerCase()) {
                result = true;
                break;
            }
        }
        return result;
    }
    return false;
}

const platIncludesArray = (arr, paths) => {
    if (arr instanceof Array) {
        let result = false;
        // eslint-disable-next-line no-plusplus
        for (let i = 0; i < arr.length; i++) {
            const ele = arr[i];
            // eslint-disable-next-line no-plusplus
            for (let j = 0; j < paths.length; j++) {
                const path = paths[j];
                if (ele.toLowerCase() === path.toLowerCase()) {
                    result = true;
                    break;
                }
            }

        }
        return result;
    }
    return false;
}

module.exports = {
    GetPkey,
    GetQueryUrlString,
    SetCookie: SetCookie,
    GetCookie: GetCookie,
    Guid: Guid,
    ControlFormart: ControlFormart,
    PickAttr: PickAttr,
    getChildCList,
    getCList,
    buildTableHeader,
    dateFormat,
    StopEvent,
    dateDiff,
    getValue,
    toAntPagination,
    GetSubtract,
    LoadTree,
    platIncludes,
    platIncludesArray
}