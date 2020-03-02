import { Form, Input, Modal, Spin, Radio, Row, Col, Button, Tag, Icon, message } from 'antd';
import React from 'react';
import OrgCom from './ChooseOrg/Index'
import RoleCom from './ChooseRole/Index'

const FormItem = Form.Item;

class CreateForm extends React.Component {
  state = {
    orgMoadlShow: false,
    roleModalShow: false,
    orgList: [],
  }


  RoleModalShow = () => {
    this.setState({
      roleModalShow: true
    })
  }

  RoleModalClose = () => {
    this.setState({
      roleModalShow: false
    })
  }


  SetRoleList = (data) => {
    this.props.SetRoleList(data);
  }


  RoleRemove = (e, item) => {
    let { roleList } = this.props;
    roleList = roleList.filter(e => e.id !== item.id)
    this.props.SetRoleList(roleList);
    e.preventDefault();
  }

  OrgModalShow = () => {
    this.setState({
      orgMoadlShow: true
    })
  }

  OrgModalClose = () => {
    this.setState({
      orgMoadlShow: false
    })
  }

  SetOrgList = (data) => {
    this.props.SetOrgList(data);
  }

  OrgRemove = (e, item) => {
    let { orgList } = this.props;
    orgList = orgList.filter(e => e.id !== item.id)
    this.props.SetOrgList(orgList);
    e.preventDefault();
  }

  render() {
    const { optMode, modalVisible, updateLoading, form, handleAdd, handleModalVisible, dataInfo, orgList, roleList } = this.props;

    const okHandle = () => {
      form.validateFields((err, fieldsValue) => {
        if (err) return;

        // eslint-disable-next-line no-shadow
        const { orgList ,roleList} = this.props;
        if (orgList.length <= 0) {
          message.error('请选择机构')
          return
        }
        if(roleList.length<=0){
          message.error('请选择角色');
          return;
        }
        
        // form.resetFields();
        const organizationIds = [];
        const roleIds=[];
        orgList.map(e => organizationIds.push(e.id));
        roleList.map(e=>roleIds.push(e.id));
        handleAdd({ ...fieldsValue, ...{ organizationIds ,roleIds} });
      });
    };


    const orgProps = {
      OrgModalClose: this.OrgModalClose,
      OrgModalOk: this.SetOrgList,
      orgMoadlShow: this.state.orgMoadlShow,
      choosedOrgList: this.props.orgList
    }
    const roleProps = {
      roleModalShow: this.state.roleModalShow,
      SetRoleList: this.SetRoleList,
      RoleModalShow: this.RoleModalShow,
      RoleModalClose: this.RoleModalClose,
      roleList:this.props.roleList
    }
    return (
      <Modal
        destroyOnClose
        width={750}
        title="编辑员工"
        visible={modalVisible}
        onOk={okHandle}
        onCancel={() => handleModalVisible()}
      >
        {
          this.state.orgMoadlShow && <OrgCom {...orgProps}></OrgCom>
        }
        {
          this.state.roleModalShow && <RoleCom {...roleProps}></RoleCom>
        }
        <Spin spinning={updateLoading}>
          {/* <FormItem
            labelCol={{
              span: 5,
            }}
            wrapperCol={{
              span: 15,
            }}
            label="UserId"
          >
            {form.getFieldDecorator('userId', {
              rules: [
                {
                  required: true,
                  message: '请输入UserId',
                  min: 1,
                  max: 20,
                },
              ],
              initialValue: dataInfo.userId,
            })(<Input placeholder="请输入UserId" />)}
          </FormItem> */}

          <FormItem
            labelCol={{
              span: 5,
            }}
            wrapperCol={{
              span: 15,
            }}
            label="姓名"
          >
            {form.getFieldDecorator('name', {
              rules: [
                {
                  required: true,
                  message: '请输入姓名',
                  min: 1,
                  max: 20,
                },
              ],
              initialValue: dataInfo.name,
            })(<Input placeholder="请输入姓名" />)}
          </FormItem>
          <Row style={{ lineHeight: '32px', marginBottom: 24 }}>
            <Col span={5} style={{ textAlign: 'right' }}>部门：</Col>
            <Col span={15}>
              {
                orgList && orgList.map(ele =>
                  <Tag color="#1890ff" onClose={e => this.OrgRemove(e, ele)} closable><Icon type="apartment" /> {ele.name}</Tag>)
              }


              <a onClick={this.OrgModalShow}>修改</a>
            </Col>
          </Row>

          <Row style={{ lineHeight: '32px', marginBottom: 24 }}>
            <Col span={5} style={{ textAlign: 'right' }}>角色：</Col>
            <Col span={15}>
              {
                roleList && roleList.map(ele =>
                  <Tag color="#1890ff" onClose={e => this.RoleRemove(e, ele)} closable><Icon type="apartment" /> {ele.name}</Tag>)
              }


              <a onClick={this.RoleModalShow}>修改</a>
            </Col>
          </Row>
          {optMode === 'add' && <FormItem
            labelCol={{
              span: 5,
            }}
            wrapperCol={{
              span: 15,
            }}
            label="密码"
          >
            {form.getFieldDecorator('password', {
              rules: [
                {
                  required: true,
                  message: '请输入密码',
                  min: 1,
                },
              ],
              initialValue: dataInfo.password,
            })(<Input.Password placeholder="请输入密码" />)}
          </FormItem>}


          <FormItem
            labelCol={{
              span: 5,
            }}
            wrapperCol={{
              span: 15,
            }}
            label="性别"
          >
            {form.getFieldDecorator('gender', {
              rules: [
                {
                  required: true
                },
              ],
              initialValue: dataInfo.gender,
            })(<Radio.Group  >
              <Radio value={1}>男</Radio>
              <Radio value={2}>女</Radio>
            </Radio.Group>)}
          </FormItem>

          <FormItem
            labelCol={{
              span: 5,
            }}
            wrapperCol={{
              span: 15,
            }}
            label="手机号"
          >
            {form.getFieldDecorator('mobile', {
              rules: [
                {
                  required: true,
                  message: '请输入手机号',
                  min: 1,
                  max: 11,
                },
              ],
              initialValue: dataInfo.mobile,
            })(<Input placeholder="请输入手机号" />)}
          </FormItem>

          <FormItem
            labelCol={{
              span: 5,
            }}
            wrapperCol={{
              span: 15,
            }}
            label="座机"
          >
            {form.getFieldDecorator('telephone', {
              rules: [
                {
                  required: true,
                  message: '请输入座机',
                  min: 1,
                  max: 20,
                },
              ],
              initialValue: dataInfo.telephone,
            })(<Input placeholder="请输入手机号" />)}
          </FormItem>



          <FormItem
            labelCol={{
              span: 5,
            }}
            wrapperCol={{
              span: 15,
            }}
            label="邮箱"
          >
            {form.getFieldDecorator('email', {
              rules: [
                {
                  required: true,
                  message: '请输入邮箱',
                  min: 1,
                  max: 20,
                },
              ],
              initialValue: dataInfo.email,
            })(<Input placeholder="请输入邮箱" />)}
          </FormItem>


          <FormItem
            labelCol={{
              span: 5,
            }}
            wrapperCol={{
              span: 15,
            }}
            label="职位"
          >
            {form.getFieldDecorator('position', {
              rules: [
                {
                  required: true,
                  message: '请输入职位',
                  min: 1,
                  max: 20,
                },
              ],
              initialValue: dataInfo.position,
            })(<Input placeholder="请输入职位" />)}
          </FormItem>

        </Spin>
      </Modal>
    );
  }

}

export default Form.create()(CreateForm);
