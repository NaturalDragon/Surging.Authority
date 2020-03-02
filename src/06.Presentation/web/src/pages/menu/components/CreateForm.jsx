import { Form, Input, Modal, Spin, TreeSelect, Select,InputNumber } from 'antd';
import React from 'react';

const FormItem = Form.Item;
const { TreeNode } = TreeSelect;
const { Option } = Select;

const treeData = [
  {
    title: 'Node1',
    value: '0-0',
    key: '0-0',
    children: [
      {
        title: 'Child Node1',
        value: '0-0-1',
        key: '0-0-1',
      },
      {
        title: 'Child Node2',
        value: '0-0-2',
        key: '0-0-2',
      },
    ],
  },
  {
    title: 'Node2',
    value: '0-1',
    key: '0-1',
  },
];
const CreateForm = props => {
  const { modalVisible, updateLoading, form, handleAdd, handleModalVisible, dataInfo, moduleList, menuTreeList } = props;

  const okHandle = () => {
    form.validateFields((err, fieldsValue) => {
      if (err) return;
      //form.resetFields();
      handleAdd(fieldsValue);
    });
  };

  return (
    <Modal
      destroyOnClose
      title="编辑菜单"
      visible={modalVisible}
      onOk={okHandle}
      onCancel={() => handleModalVisible()}
    >
      <Spin spinning={updateLoading}>
        <FormItem
          labelCol={{
            span: 5,
          }}
          wrapperCol={{
            span: 15,
          }}
          label="菜单名称"
        >
          {form.getFieldDecorator('name', {
            rules: [
              {
                required: true,
                message: '请输入菜单名称！',
                min: 1,
                max: 20,
              },
            ],
            initialValue: dataInfo.name,
          })(<Input placeholder="请输入菜单名称" />)}
        </FormItem>
        <FormItem
          labelCol={{
            span: 5,
          }}
          wrapperCol={{
            span: 15,
          }}
          label="图标"
        >
          {form.getFieldDecorator('icon', {
            rules: [
              {

                min: 1,
                max: 20,
              },
            ],
            initialValue: dataInfo.icon,
          })(<Input placeholder="请输入菜单名称" />)}
        </FormItem>
        <FormItem
          labelCol={{
            span: 5,
          }}
          wrapperCol={{
            span: 15,
          }}
          label="排序"
        >
          {form.getFieldDecorator('sortIndex', {
            rules: [
              {
                required: true,
              
              },
            ],
            initialValue: dataInfo.sortIndex,
          })(<InputNumber min={1} max={999} placeholder="请输入序号" />)}
        </FormItem>
        <FormItem
          labelCol={{
            span: 5,
          }}
          wrapperCol={{
            span: 15,
          }}
          label="父级菜单"
        >
          {form.getFieldDecorator('parentId', {
            rules: [
              {
                required: true,
                message: '请选择父级菜单',

              },
            ],
            initialValue: dataInfo.parentId,
          })(<TreeSelect style={{ width: '100%' }}
            dropdownStyle={{ maxHeight: 400, overflow: 'auto' }}
            treeData={menuTreeList}
            placeholder="请选择父级菜单"
            treeDefaultExpandAll
          > </TreeSelect>)}
        </FormItem>
        <FormItem
          labelCol={{
            span: 5,
          }}
          wrapperCol={{
            span: 15,
          }}
          label="关联模块"
        >
          {form.getFieldDecorator('moduleId', {
            rules: [
              {
              },
            ],
            initialValue: dataInfo.moduleId,
          })(<Select style={{ width: '100%' }} placeholder="请选择关联模块">
            {
              moduleList && moduleList.map(ele => <Option value={ele.id}>{ele.name}</Option>)
            }


          </Select>)}
        </FormItem>
      </Spin>
    </Modal>
  );
};

export default Form.create()(CreateForm);
