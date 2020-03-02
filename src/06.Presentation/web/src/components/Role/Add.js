import { Form, Input, Modal, Spin, Button, Select } from 'antd';
import React from 'react';

const FormItem = Form.Item;
const { Option } = Select

const CreateForm = props => {
  const { roleTypeList,dataInfo, modifyLoading, actioning, addVisible, addOk, addCancel, form } = props;

  const okHandle = () => {
    form.validateFields((err, fieldsValue) => {
      if (err) return;
      addOk(fieldsValue);
    });
  };

  return (
    <Modal
      destroyOnClose
      title="编辑角色"
      visible={addVisible}
      onOk={okHandle}
      onCancel={() => addCancel()}
      footer={[
        <Button onClick={() => addCancel()}>取消</Button>,
        <Button loading={actioning} onClick={okHandle} type='primary'>确定</Button>
      ]}
    >
      <Spin spinning={modifyLoading}>

      <FormItem
          labelCol={{
            span: 5,
          }}
          wrapperCol={{
            span: 15,
          }}
          label="角色类型"
        >
          {form.getFieldDecorator('roleType', {
            rules: [
              {
                required: true,
                message: '请选择角色类型！',
              },
            ],
            initialValue: dataInfo.roleType,
          })(<Select >
            {
              roleTypeList&&roleTypeList.map(ele=>
              <Option value={ele.id}>{ele.name}</Option>)
            }
          </Select>)}
        </FormItem>
        <FormItem
          labelCol={{
            span: 5,
          }}
          wrapperCol={{
            span: 15,
          }}
          label="角色名称"
        >
          {form.getFieldDecorator('name', {
            rules: [
              {
                required: true,
                message: '请输入角色名称！',
                min: 1,
                max: 20,
              },
            ],
            initialValue: dataInfo.name,
          })(<Input placeholder="请输入角色名称" />)}
        </FormItem>

      </Spin>
    </Modal>
  );
};

export default Form.create()(CreateForm);
