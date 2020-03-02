/* eslint-disable no-restricted-syntax */
import {
  Button,
  Card,
  Col,
  Form,
  Icon,
  Input,
  Row,
  Popconfirm
} from 'antd';
import React, { Component, Fragment } from 'react';
import { PageHeaderWrapper } from '@ant-design/pro-layout';
import { connect } from 'dva';
import CreateForm from './components/CreateForm';
import StandardTable from './components/StandardTable';
import { pageSize } from '../../utils/config'
import styles from './style.less';
import { getValue, Guid } from '../../utils/com'
import PlatFormA from '@/components/PlatFormA'
import injectAuth from '@/components/injectAuth'
import { permissionJson } from '@/utils/permission'

const AuthorA = injectAuth(PlatFormA)
const AuthorButton = injectAuth(Button)
const permission = permissionJson.module
const FormItem = Form.Item;


/* eslint react/no-multi-comp:0 */
@connect(({ moduleModel, loading }) => ({
  moduleModel,
  loading: loading.models.moduleModel,
}))
class TableList extends Component {
  state = {
    // eslint-disable-next-line react/no-unused-state
    optMode: 'add', // add,modify
    operating: false,
    updateLoading: false,
    modalVisible: false,
    expandForm: false,
    selectedRows: [],
    formValues: {},
    // eslint-disable-next-line react/no-unused-state
    currentItem: {},
  };

  columns = [
    {
      title: '模块名称',
      dataIndex: 'name',
    },
    {
      title: 'URL',
      dataIndex: 'url',
      width:400
    },
    {
      title: '可用',
      dataIndex: 'isEnabled',
      width:100,
      render: isEnabled => {
        let t=JSON.stringify(isEnabled)
      return <span>{t}</span>
      }
    },
    
    {
      title: '创建时间',
      dataIndex: 'createDate',
      sorter: true,
    },
    {
      title: '操作',
      render: (text, record) => (
        <Fragment>
          <AuthorA  auth={permission.modify} onClick={() => this.handleUpdateModalVisible(true, record)}>修改</AuthorA>
        </Fragment>
      ),
    },
  ];

  componentDidMount() {
    const { dispatch } = this.props;
    dispatch({
      type: 'moduleModel/fetch',
      payload: { pageIndex: 1, pageSize },
    });
  }

  handleStandardTableChange = (pagination, filtersArg, sorter) => {
    const { dispatch } = this.props;
    const { formValues } = this.state;
    const filters = Object.keys(filtersArg).reduce((obj, key) => {
      const newObj = { ...obj };
      newObj[key] = getValue(filtersArg[key]);
      return newObj;
    }, {});
    const params = {
      pageIndex: pagination.current,
      pageSize: pagination.pageSize,
      ...formValues,
      ...filters,
    };

    if (sorter.field) {
      params.sorter = `${sorter.field}_${sorter.order}`;
    }

    dispatch({
      type: 'moduleModel/fetch',
      payload: params,
    });
  };

  handleSearch = e => {
    e.preventDefault();
    const { dispatch, form, moduleModel: { pagination } } = this.props;
    form.validateFields((err, fieldsValue) => {
      if (err) return;
      const values = {
        ...fieldsValue,
        updatedAt: fieldsValue.updatedAt && fieldsValue.updatedAt.valueOf(),
      };
      this.setState({
        formValues: values,
      });
      dispatch({
        type: 'moduleModel/fetch',
        payload: { ...values, ...{ pageIndex: pagination.current, pageSize: pagination.pageSize } },
      });
    });
  };

  handleFormReset = () => {
    const { form, dispatch, moduleModel: { pagination } } = this.props;
    form.resetFields();
    this.setState({
      formValues: {},
    });
    dispatch({
      type: 'moduleModel/fetch',
      payload: { pageIndex: pagination.current, pageSize: pagination.pageSize },
    });
  };

  toggleForm = () => {
    const { expandForm } = this.state;
    this.setState({
      expandForm: !expandForm,
    });
  };

  handleRemoveClick = () => {
    const { dispatch } = this.props;
    const { selectedRows } = this.state;
    if (!selectedRows) return;
    dispatch({
      type: 'moduleModel/remove',
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

  handleSelectRows = rows => {
    this.setState({
      selectedRows: rows,
    });
  };

  handleModalVisible = flag => {
    this.setState({
      optMode: 'add',
      // eslint-disable-next-line react/no-unused-state
      operating: false,
      modalVisible: !!flag,
    });
    if(!!flag){
      this.props.dispatch({
        type: 'moduleModel/clear',
      })
    }
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
      type: 'moduleModel/getForModfiy',
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

  handleAdd = fields => {
    const { dispatch } = this.props;

    this.setState({ operating: true })
    if (this.state.optMode === 'add') {

      dispatch({
        type: 'moduleModel/create',
        payload: {
          ...{ ...fields },
        },
        callback: () => {
          this.handleModalVisible();
          this.handleFormReset();
        },
      });
    } else {
      dispatch({
        type: 'moduleModel/modify',
        payload: {
          ...{ ...fields, ...{ id: this.state.currentItem.id } }
        },
        callback: () => {
          this.handleModalVisible();
          this.handleFormReset()
        },
      });
    }
    // this.handleModalVisible();
  };

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
            <FormItem label="模块名称">
              {getFieldDecorator('name')(<Input placeholder="请输入" />)}
            </FormItem>
          </Col>
         
          <Col md={8} sm={24}>
            <span className={styles.submitButtons}>
              <AuthorButton auth={permission.list} type="primary" htmlType="submit">
                查询
              </AuthorButton>
              <AuthorButton auth={permission.list}
                style={{
                  marginLeft: 8,
                }}
                onClick={this.handleFormReset}
              >
                重置
              </AuthorButton>
              <a
                style={{
                  marginLeft: 8,
                }}
                onClick={this.toggleForm}
              >
                展开 <Icon type="down" />
              </a>
            </span>
          </Col>
        </Row>
      </Form>
    );
  }

  renderAdvancedForm() {
    const {
      form: { getFieldDecorator },
    } = this.props;
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
            <FormItem label="类型名称">
              {getFieldDecorator('name')(<Input placeholder="请输入" />)}
            </FormItem>
          </Col>
        </Row>
        <div
          style={{
            overflow: 'hidden',
          }}
        >
          <div
            style={{
              float: 'right',
              marginBottom: 24,
            }}
          >
             <AuthorButton auth={permission.list} type="primary" htmlType="submit">
              查询
            </AuthorButton>
            <AuthorButton auth={permission.list}
              style={{
                marginLeft: 8,
              }}
              onClick={this.handleFormReset}
            >
              重置
            </AuthorButton>
            <a
              style={{
                marginLeft: 8,
              }}
              onClick={this.toggleForm}
            >
              收起 <Icon type="up" />
            </a>
          </div>
        </div>
      </Form>
    );
  }

  renderForm() {
    const { expandForm } = this.state;
    return expandForm ? this.renderAdvancedForm() : this.renderSimpleForm();
  }

  render() {
    const {
      moduleModel: { list, pagination, dataInfn },
      loading,
    } = this.props;
    const data = { list, pagination };
    const { selectedRows, updateLoading, modalVisible } = this.state;

    const parentMethods = {
      dataInfn,
      handleAdd: this.handleAdd,
      handleModalVisible: this.handleModalVisible,
    };
    return (
      <PageHeaderWrapper>
        <Card bordered={false}>
          <div className={styles.tableList}>
            <div className={styles.tableListForm}>{this.renderForm()}</div>
            <div className={styles.tableListOperator}>
              <AuthorButton  auth={permission.new} icon="plus" type="primary" onClick={() => this.handleModalVisible(true)}>
                新建
              </AuthorButton>
              {selectedRows.length > 0 && (
                <Popconfirm
                  title="确定删除吗?"
                  onConfirm={this.handleRemoveClick}
                  // onCancel={cancel}
                  okText="确定"
                  cancelText="取消"
                >
                  <span>
                    <AuthorButton auth={permission.remove}>删除</AuthorButton>

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
              onChange={this.handleStandardTableChange}
            />
          </div>
        </Card>
        <CreateForm {...parentMethods} operating={this.state.operating} updateLoading={updateLoading} modalVisible={modalVisible} />
      </PageHeaderWrapper>
    );
  }
}

export default Form.create()(TableList);
