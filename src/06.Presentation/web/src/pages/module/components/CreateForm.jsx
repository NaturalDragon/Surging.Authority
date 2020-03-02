import { Form, Input, Modal, Spin, Switch, Row, Col, Card, Button, Icon } from 'antd';
import React, { Fragment } from 'react';
import { Guid, GetSubtract } from '@/utils/com'

const FormItem = Form.Item;
let id = 0;
let ElementList = []
function getKeys(elementList) {
  const keysList = [];
  elementList.forEach((ele, index) => { keysList.push(index) });
  return keysList;

}
const a = 2;;
const colProps={
  labelCol:{
    span: 5,
  },
  wrapperCol:{
    span: 5,
  }
}
// eslint-disable-next-line react/prefer-stateless-function
class FormItems extends React.Component {

  render() {
    const { elementList, getFieldDecorator, remove } = this.props;
    return (<Fragment>
      {
      elementList.map((ele, k) => {
          if (ele.operationStatus === 1 || ele.operationStatus === 2) {
            return <Row key={`${ele.id}-row`}>
              <Col span={9}>
                <Form.Item
                  labelCol={{
                    span: 5,
                  }}
                  wrapperCol={{
                    span: 15,
                  }}
                  label='资源名称'
                  required={false}
                // eslint-disable-next-line react/no-array-index-key

                >
                  {getFieldDecorator(`names[${ele.id}]`, {
                    validateTrigger: ['onChange', 'onBlur'],
                    rules: [
                      {
                        required: true,
                        whitespace: true,
                        message: "请输入资源名称",
                      },

                    ],
                    initialValue: ele.name//ElementList[k].name
                  })(<Input placeholder="请输入资源名称" style={{ width: '60%', marginRight: 8 }} />)}

                </Form.Item>
              </Col>
              <Col span={15}>
                <Form.Item
                  labelCol={{
                    span: 5,
                  }}
                  wrapperCol={{
                    span: 15,
                  }}
                  label='路由路径'
                  required={false}

                >
                  {getFieldDecorator(`routePaths[${ele.id}]`, {
                    validateTrigger: ['onChange', 'onBlur'],
                    rules: [
                      {
                        required: true,
                        whitespace: true,
                        message: "请输入路由路径",
                      },
                    ],
                    initialValue: ele.routePath
                  })(<Input placeholder="请输入路由路径" style={{ width: '90%', marginRight: 8 }} />)}
                  <Icon
                    className="dynamic-delete-button"
                    type="minus-circle-o"
                    onClick={() => remove(k)}
                  />
                </Form.Item>
              </Col>
            </Row>
          } else {
            return null;
          }
        })
      }
    </Fragment>
    );
  }
}


class CreateForm extends React.Component {

  constructor(props) {
    super(props)

  }

  componentDidMount() {
    const { form } = this.props;
    const keysList = getKeys(ElementList);
    form.setFieldsValue({
      keys: keysList,
    });
  }

  setElementList = () => {
    const { form } = this.props;
    const keyValues = form.getFieldsValue();
    const { names, routePaths } = keyValues;
    if (names && routePaths) {
      ElementList.map((ele, index) => {
        ele.name = names[ele.id];
        ele.routePath = routePaths[ele.id];

      });
    }
  }

  remove = k => {
    const { form } = this.props;
    this.setElementList()
    ElementList.forEach((ele, index) => {
      if (index === k) {
        ele.operationStatus = ele.operationStatus === 1 ? 0 : 3;
      }

    })
    const keysList = getKeys(ElementList);
    form.setFieldsValue({
      keys: keysList
    });
  };

  add = () => {

    const { form, dataInfn } = this.props;
    this.setElementList();
    ElementList.push({ id: Guid(), moduleId: dataInfn.id, name: '', routePath: '', operationStatus: 1 })
    const keysList = getKeys(ElementList);
    form.setFieldsValue({
      keys: keysList,
    });
  };

  handleSubmit = e => {
    const { form, dataInfn } = this.props;

    form.validateFields((err, fieldsValue) => {
      if (err) return;
      this.setElementList();
      const eleList = ElementList;
      const {
        keys,
        names,
        routePaths,
        ...postJson

      } = fieldsValue;
      let index=0;
      ElementList.forEach(ele=>{
        if(ele.operationStatus===1||ele.operationStatus===2){
          index++;
          ele.sortIndex=index;
        }
      })
      this.props.handleAdd({
        ...postJson, ...{
          id: dataInfn.id,
          operationStatus: dataInfn.operationStatus, code: 1
          , moduleElementActionRequests: eleList
        }
      });
    });
  };

  render() {
    const { modalVisible, updateLoading, form, operating, handleAdd, handleModalVisible, dataInfn } = this.props;
    const { getFieldDecorator, getFieldValue } = this.props.form;
    debugger
    ElementList = this.props.dataInfn.moduleElementActionRequests;

    getFieldDecorator('keys', { initialValue: [] });
    const keys = getFieldValue('keys');

    return (
      <Modal
        destroyOnClose
        title="编辑模块"
        width='90%'
        visible={modalVisible}
        // onOk={this.handleSubmit}
        onCancel={() => handleModalVisible()}

        footer={[<Button onClick={() => handleModalVisible()} type='default'>取消</Button>
          ,
        <Button onClick={this.handleSubmit} loading={operating} type='primary'>确定</Button>]
        }
      >
        <Spin spinning={updateLoading}>
          <FormItem
           {...colProps}
            label="模块名称"
          >
            {form.getFieldDecorator('name', {
              rules: [
                {
                  required: true,
                  message: '请输入模块名称',
                  min: 1,
                  max: 20,
                },
              ],
              initialValue: dataInfn.name,
            })(<Input placeholder="请输入模块名称" />)}
          </FormItem>
          <FormItem
             {...colProps}
            label="URL"
          >
            {form.getFieldDecorator('url', {
              rules: [
                {
                  // required: true,
                  message: '请输入URL',
                  // min: 1,
                  max: 128,
                },
              ],
              initialValue: dataInfn.url,
            })(<Input placeholder="请输入URL" />)}
          </FormItem>
          <FormItem
           {...colProps}
            label="启用"
          >
            {form.getFieldDecorator('isEnabled', {
              valuePropName: 'checked',
              initialValue: dataInfn.isEnabled,
            })(<Switch />)}
          </FormItem>
          {/* {formItems} */}
          <Row>
            <Col style={{ textAlign: 'right' }} span={5}>权限资源：</Col>
            <Col span={15}>
              <Card bodyStyle={{ padding: '24px 10px  24px 10px' }}>
                <Row>
                  <Col span={20}>

                  </Col>
                  <Col span={4}>
                    <Button onClick={this.add} type='primary'> <Icon type="plus" />新增</Button>
                  </Col>
                </Row>
                <FormItems elementList={ElementList} getFieldDecorator={getFieldDecorator} remove={this.remove} />
              
              </Card>
            </Col>
          </Row>
        </Spin>
      </Modal>
    );
  }
}

export default Form.create()(CreateForm);
