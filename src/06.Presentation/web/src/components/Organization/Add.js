import { Form, Input, Modal, Spin, TreeSelect, Select } from 'antd';
import React from 'react';

const FormItem = Form.Item;

const CreateForm = props => {
  const { dataInfo,modifyLoading,addVisible,addOk,addCancel,form} = props;

  const okHandle = () => {
    form.validateFields((err, fieldsValue) => {
      if (err) return;
      addOk(fieldsValue);
    });
  };

  return (
    <Modal
      destroyOnClose
      title="编辑机构"
      visible={addVisible}
      onOk={okHandle}
      onCancel={() => addCancel()}
    >
      <Spin spinning={modifyLoading}>
        <FormItem
          labelCol={{
            span: 5,
          }}
          wrapperCol={{
            span: 15,
          }}
          label="机构名称"
        >
          {form.getFieldDecorator('name', {
            rules: [
              {
                required: true,
                message: '请输入机构名称！',
                min: 1,
                max: 20,
              },
            ],
            initialValue: dataInfo.name,
          })(<Input placeholder="请输入机构名称" />)}
        </FormItem>
     
      </Spin>
    </Modal>
  );
};

export default Form.create()(CreateForm);
