
// 公司注册类型
const companyRegisterType = [
  { name: '国有', value: 1 },
  { name: '合资', value: 2 },
  { name: '独资', value: 3 },
]
const controls = ['undo', 'redo',
  'font-size', 'line-height', 'letter-spacing',
  'text-color', 'bold', 'italic', 'underline', 'strike-through',
  'superscript', 'subscript', 'remove-styles', 'text-indent', 'text-align',
  'list-ul', 'list-ol', 'blockquote',
  'link', 'hr',
  'clear', 'media']


const publishList = [
  { name: '未发布', value: 0 },
  { name: '已发布', value: 1 },
]

const topList = [
  { name: '否', value: 0 },
  { name: '是', value: 1 },
]

// 来源
const sourceType = [
  {
    name: '自有',
    value: 0,
  },
  {
    name: '外租',
    value: 1,
  }
]
// 证书情况
const currentType = [
  {
    name: '良好',
    value: 0,
  },
  {
    name: '故障',
    value: 1,
  },
  {
    name: '维修',
    value: 1,
  }
]
// 证书情况
const hasCertificationType = [
  {
    name: '有',
    value: true,
  },
  {
    name: '无',
    value: false,
  }
]
// 订单状态(0：未支付，1：已支付，2：申请退款，3：退款成功，4：驳回退款，5：关闭 )
const orderStatusList = [
  { name: '未支付', value: 0 },
  { name: '已支付', value: 1 },
  { name: '申请退款', value: 2 },
  { name: '退款成功', value: 3 },
  { name: '驳回退款', value: 4 },
  { name: '关闭', value: 5 },
]
// 订单类型
const orderTypeList = [
  { name: '认证订单', value: 0 },
  { name: '续费订单', value: 1 },
]

const authenticationStepList = [
  { name: '初始化', value: 0 },
  { name: '认证支付成功', value: 1 },
  { name: '上传营业执照', value: 2 },
  { name: '认证成功', value: 3 },
]

const authorAction={
  organization:'organization',
  role:'role',
  employee:'employee'
}

module.exports = {
  companyRegisterType,
  controls,
  publishList,
  topList,
  orderStatusList,
  orderTypeList,
  sourceType,
  currentType,
  hasCertificationType,
  authenticationStepList,
  authorAction
}
