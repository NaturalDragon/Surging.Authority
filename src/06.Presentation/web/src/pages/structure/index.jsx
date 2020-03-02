import {
  Tabs,
  Button,
  Card,
  Col,
  Form,
  Icon,
  Input,
  Divider,
  Row,
  Popconfirm,
  Tree,
  Table,
  Checkbox
} from 'antd';
import React, { Component, Fragment } from 'react';
import { PageHeaderWrapper } from '@ant-design/pro-layout';
import { connect } from 'dva';
import CreateForm from './components/CreateForm';
import StandardTable from './components/StandardTable';
import styles from './style.less';
import { getValue, Guid } from '@/utils/com'

import OrganizationCom from '@/components/Organization/Index'
import RoleCom from '@/components/Role/Index'
import PlatFormA from '@/components/PlatFormA'
import injectAuth from '@/components/injectAuth'
import { permissionJson } from '@/utils/permission'

const AuthorA = injectAuth(PlatFormA)
const AuthorButton = injectAuth(Button)
const permission = permissionJson.structure
const { TabPane } = Tabs;
const FormItem = Form.Item;
const { TreeNode, DirectoryTree } = Tree;


/* eslint react/no-multi-comp:0 */
@connect(({ structureModel, loading }) => ({
  structureModel,
  loading: loading.models.structureModel,
}))
class PowerRouteCom extends Component {
  state = {
    optMode: 'add',
    // eslint-disable-next-line react/no-unused-state
    modalVisible: false,
    selectedRows: [],
    updateLoading: false,
    expandForm: false,
    // eslint-disable-next-line react/no-unused-state
    currentItem: {},
    currentOrg: {},
    currentManage:{}
  };

  columns = [
    {
      title: '账号',
      dataIndex: 'userId',
      render: text => <a>{text}</a>,
    },
    {
      title: '姓名',
      dataIndex: 'name',
      render: text => <a>{text}</a>,
    },
    {
      title: '职务',
      dataIndex: 'position',
    },
    {
      title: '角色',
      dataIndex: 'roleName',
    },
    {
      title: '部门',
      dataIndex: 'department',
    },
    {
      title: '手机',
      dataIndex: 'mobile',
    },
    {
      title: '邮箱',
      dataIndex: 'email',
    },
    {
      title: '操作',
      render: (text, record) => (
        <Fragment>
          <AuthorA auth={permission.empModify} onClick={() => this.handleUpdateModalVisible(true, record)}>修改</AuthorA>
        </Fragment>
      ),
    },
  ];

  handleSearch = e => {
    e.preventDefault();
    const { dispatch, form, structureModel: { pagination } } = this.props;
    form.validateFields((err, fieldsValue) => {
      if (err) return;
      const values = {
        ...fieldsValue,
        updatedAt: fieldsValue.updatedAt && fieldsValue.updatedAt.valueOf(),
      };
      this.setState({
        formValues: values,
        currentManage:null,
      });
      dispatch({
        type: 'structureModel/fetch',
        payload: { ...values, ...{isKeySearch:true, pageIndex: 1, pageSize: pagination.pageSize } },
      });
    });
  };

  getEmployee = (payload) => {
   // console.log(payload);
    this.setState({
      currentManage:payload
    })
    this.props.dispatch({
      type: 'structureModel/fetch',
      payload: {
        pageIndex: 1, pageSize: 10,
        ...payload
      }
    })
  }

  setCurrentOrg = (orgItem) => {
    this.setState({
      currentOrg: orgItem
    })
  }

  handleStandardTableChange = (pagination, filtersArg, sorter) => {
    const { dispatch } = this.props;
    const { formValues,currentManage } = this.state;
    const filters = Object.keys(filtersArg).reduce((obj, key) => {
      const newObj = { ...obj };
      newObj[key] = getValue(filtersArg[key]);
      return newObj;
    }, {});
    const params = {
      isKeySearch:currentManage==null,
      pageIndex: pagination.current,
      pageSize: pagination.pageSize,
      organizationId:currentManage!=null?currentManage.organizationId:"",
      roleId:currentManage!=null?currentManage.roleId:'',
      ...formValues,
      ...filters,
    };

    if (sorter.field) {
      params.sorter = `${sorter.field}_${sorter.order}`;
    }

    dispatch({
      type: 'structureModel/fetch',
      payload: params,
    });
  };


  handleSelectRows = rows => {
    this.setState({
      selectedRows: rows,
    });
  };

  toggleForm = () => {
    const { expandForm } = this.state;
    this.setState({
      expandForm: !expandForm,
    });
  };

  // eslint-disable-next-line react/sort-comp
  renderSimpleForm() {
    const { form } = this.props;
    const { getFieldDecorator } = form;
    return (
      <Form onSubmit={this.handleSearch} layout="inline">
        <Row
          gutter={{
            md: 8,
            lg: 24,
            xl: 48,
          }}
        >
          <Col md={8} sm={24}>
            <FormItem label="关键字">
              {getFieldDecorator('queryKey')(<Input placeholder="姓名、职务、手机号、邮箱" />)}
            </FormItem>
          </Col>
          <Col md={8} sm={24}>
            <span className={styles.submitButtons}>
              <AuthorButton auth={permission.empList} type="primary" htmlType="submit">
                查询
              </AuthorButton>
              <AuthorButton auth={permission.empList}
                style={{
                  marginLeft: 8,
                }}
                onClick={this.handleFormReset}
              >
                重置
              </AuthorButton>
            </span>
          </Col>
        </Row>
      </Form>
    );
  }

  handleFormReset = () => {
    const { form, dispatch, structureModel: { pagination } } = this.props;
    form.resetFields();
    this.setState({
      formValues: {},
      currentManage:null,
    });
    dispatch({
      type: 'structureModel/fetch',
      payload: {isKeySearch:true, pageIndex: 1, pageSize: pagination.pageSize },
    });
  };

  handleAdd = fields => {
    const { dispatch } = this.props;
    if (this.state.optMode === 'add') {
      dispatch({
        type: 'structureModel/add',
        payload: {
          ...{ ...fields, ...{ id: Guid(), department: '', order: 1,operationStatus:1 } },
        },
        callback: () => {
          this.handleModalVisible();
          this.handleFormReset();
        },
      });
    } else {
      dispatch({
        type: 'structureModel/modify',
        payload: {
          ...{ ...fields, ...{ id: this.state.currentItem.id, department: '', order: 1,operationStatus:2 } }
        },
        callback: () => {
          this.handleModalVisible();
          this.handleFormReset()
        },
      });
    }
    // this.handleModalVisible();
  };

  handleUpdateModalVisible = (flag, record) => {
    this.setState({
      modalVisible: !!flag,
      updateLoading: true,
      // eslint-disable-next-line react/no-unused-state
      optMode: 'modify',
      // eslint-disable-next-line react/no-unused-state
      currentItem: record,

    });
    // eslint-disable-next-line no-undef
    this.props.dispatch({
      type: 'structureModel/getForModfiy',
      payload: {
        id: record.id,
      },
      callback: () => {
        this.setState({
          updateLoading: false,
        })
      },
    })
  };

  handleRemoveClick = () => {
    const { dispatch } = this.props;
    const { selectedRows } = this.state;
    if (!selectedRows) return;
    dispatch({
      type: 'structureModel/remove',
      payload: {
        ids: selectedRows.map(row => row.id),
      },
      callback: () => {
        this.setState({
          selectedRows: [],
        });
        this.handleFormReset()
      },
    });
  };

  handleModalVisible = flag => {

    this.setState({
      optMode: 'add',
      modalVisible: !!flag,
    });
    // if(this.state.currentOrg)
    this.props.dispatch({
      type: 'structureModel/save',
      payload: { orgList: [this.state.currentOrg] }
    })
    if(!!flag){
      this.props.dispatch({
        type: 'structureModel/clear',
      })
    }
  };

  addEmployeeShow = (org, role) => {
    this.handleModalVisible(true)
    if(org){
      this.props.dispatch({
        type: 'structureModel/save',
        payload: { orgList: [org] }
      })
    }
    if (role) {
      this.props.dispatch({
        type: 'structureModel/save',
        payload: { roleList: [role] }
      })
    }
  }

  SetOrgList = (data) => {

    this.props.dispatch({
      type: 'structureModel/save',
      payload: {
        orgList: data,
      },
    })
  }

  SetRoleList = (data) => {
    this.props.dispatch({
      type: 'structureModel/save',
      payload: {
        roleList: data,
      },
    })
  }

  renderForm() {
    const { expandForm } = this.state;
    return this.renderSimpleForm();
  }

  render() {
    const { structureModel: { list, pagination, dataInfo, orgList, roleList }, loading } = this.props;

    const { selectedRows, updateLoading, modalVisible, optMode } = this.state;

    const data = { list, pagination };
    const OrgProps = {
      
      getEmployee: this.getEmployee,
      setCurrentOrg: this.setCurrentOrg,
      addEmployeeShow:this.addEmployeeShow
    }

    const RoleProps = {

      getEmployee: this.getEmployee,
      addEmployeeShow: this.addEmployeeShow
    }
    const parentMethods = {
      optMode,
      SetOrgList: this.SetOrgList,
      SetRoleList: this.SetRoleList,
      roleList,
      orgList,
      dataInfo,
      handleAdd: this.handleAdd,
      handleModalVisible: this.handleModalVisible,
    };
    return (<PageHeaderWrapper>
      <Card bordered={false} bodyStyle={{ padding: '0 24px 24px' }}>
        <div className={styles.powerWrap}>
          <div className={styles.leftCon} style={{ borderRight: '1px solid #ddd' }}>
            <Tabs defaultActiveKey="1">
              <TabPane tab="机构" key="1">
                <OrganizationCom operable={true} {...OrgProps}></OrganizationCom>
              </TabPane>
              <TabPane tab="角色" key="2">
                <RoleCom operable={true} {...RoleProps}></RoleCom>
              </TabPane>

            </Tabs>

          </div>
          <div className={styles.rightCon} span={18}>
            <div className={styles.tableListForm}>{this.renderForm()}</div>
            {/* <Divider></Divider> */}
            <div className={styles.tableListOperator}>
              {/* <Button icon="plus" type="primary" onClick={() => this.handleModalVisible(true)}>
                新建
              </Button> */}
              {selectedRows.length > 0 && (
                <Popconfirm
                  title="确定删除吗?"
                  onConfirm={this.handleRemoveClick}
                  // onCancel={cancel}
                  okText="确定"
                  cancelText="取消"
                >
                  <span>
                    <AuthorButton auth={permission.empRemove}>删除</AuthorButton>

                  </span>
                </Popconfirm>
              )}
            </div>
            <StandardTable
              selectedRows={selectedRows}
              loading={loading}
              data={data}
              columns={this.columns}
              onSelectRow={this.handleSelectRows}
              onChange={this.handleStandardTableChange}></StandardTable>

            <CreateForm {...parentMethods} updateLoading={updateLoading} modalVisible={modalVisible} />
          </div>
        </div>
      </Card>

    </PageHeaderWrapper>)
  }
}

export default Form.create()(PowerRouteCom);
