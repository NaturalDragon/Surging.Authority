/* eslint-disable react/sort-comp */
/* eslint-disable default-case */
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
import { pageSize } from '../../utils/config'
import styles from './style.less';
import { getValue, Guid } from '../../utils/com'

import OrganizationCom from '@/components/Organization/Index'
import RoleCom from '@/components/Role/Index'
import PowerDesignCom from './components/powerDesign/Index'
import PowerDesignMenuCom from './components/PowerDesignMenu/Index'
import EmployeeCom from '@/components/Employee/Index'
import { authorAction } from '@/utils/constant'

const { TabPane } = Tabs;
const FormItem = Form.Item;
const { TreeNode, DirectoryTree } = Tree;
const winHeight=window.innerHeight-188;
function ReturnHeader(currentTab) {
  let res = "";
  switch (currentTab) {
    case authorAction.organization:
      res = "机构";
      break;
    case authorAction.role:
      res = "角色";
      break;
    case authorAction.employee:
      res = "人员";
      break;
    default:
      break;
  }
  return res;
}


/* eslint react/no-multi-comp:0 */
@connect(({ powerModel, loading }) => ({
  powerModel,
  loading: loading.models.powerModel,
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
    currentTab: 'org',// org role
  };

componentDidMount(){
  this.getAllModule();
  this.getMenuTree();
}

  setCurrentOrg = (orgItem) => {
    this.setState({
      currentItem: orgItem
    })
  }


  setcurrentTab = (tab) => {
    this.setState({ currentTab: tab })
  }


  SetOrgList = (data) => {
    this.props.dispatch({
      type: 'powerModel/save',
      payload: {
        orgList: data,
      },
    })
  }


  SetRoleList = (data) => {
    this.props.dispatch({
      type: 'powerModel/save',
      payload: {
        roleList: data,
      },
    })
  }

  getAllModule = () => {
    this.props.dispatch({
      type: 'powerModel/getAllModule',
      payload: {
      }
    })
  }

  getMenuTree = () => {
    this.props.dispatch({
      type: 'powerModel/getMenuTree',
      payload: {
      }
    })
  }

  setpowerMemuLoading = () => {
    this.props.dispatch({
      type: 'powerModel/save',
      payload: {
        powerMemuLoading: true
      }
    })
  }

  getOrgMenu = (payload) => {
    this.setpowerMemuLoading();
    const { currentItem } = this.state;
    this.props.dispatch({
      type: 'powerModel/getOrgMenu',
      payload: {
        ...payload
      }
    })
    this.props.dispatch({
      type: 'powerModel/getElementPower',
      payload: {
        ...payload
      },
      currentItem
    })
  }


  setModuleList = (list) => {
    this.props.dispatch({
      type: 'powerModel/save',
      payload: {
        moduleList: list
      }
    })
  }

  setOrgMenu = (ids) => {
    this.props.dispatch({
      type: 'powerModel/save',
      payload: {
        menuCheckIds: ids
      }
    })
  }

  togglePowerElementActing=(flag)=>{

    this.props.dispatch({
      type:'powerModel/save',
      payload:{
        powerElementActing:flag
      }
    })
  }

  saveElementPower = (obj) => {
    const { currentTab, currentItem } = this.state;

    const others = {
      organizationId: currentItem.id,
      roleId: currentItem.roleId,
      employeeId: currentItem.employeeId
    }
    this.togglePowerElementActing(true)
    let actionFuc="";
    if (currentItem.action === authorAction.organization) {
      actionFuc='SaveOrganizationModuleElement';
    }else if (currentItem.action === authorAction.role) {
      actionFuc='SaveRoleModuleElement';
    }else if (currentItem.action === authorAction.employee) {
      actionFuc='SaveEmployeeModuleElement';
    }
    this.props.dispatch({
      type: `powerModel/${actionFuc}`,
      payload: {
        elementIds: obj,
        ...others
      },
      callBack:()=>{
        this.togglePowerElementActing(false)
      }
    })
  }

  togglePowerMenuActing=(flag)=>{
      this.props.dispatch({
        type:'powerModel/save',
        payload:{
          powerMenuActing:flag
        }
      })
  }

  saveRelationMenu = (obj) => {
    const { powerModel: { menuCheckIds } } = this.props
    const { currentTab, currentItem } = this.state;
    this.togglePowerMenuActing(true)
    if (currentItem.action === authorAction.organization) {
      const payload = {
        menuIds: menuCheckIds,
        organizationId: currentItem.id,
      }
      this.props.dispatch({
        type: 'powerModel/saveOrgMenu',
        payload: {
          ...payload,
        },
        callBack:()=>{
          this.togglePowerMenuActing(false)
        }
      })
    } else if (currentItem.action === authorAction.role) {
      this.props.dispatch({
        type: 'powerModel/saveRoleMenu',
        payload: {
          menuIds: menuCheckIds,
          roleId: currentItem.roleId
        },
        callBack:()=>{
          this.togglePowerMenuActing(false)
        }
      })
    } else if (currentItem.action === authorAction.employee) {
      this.props.dispatch({
        type: 'powerModel/saveEmployeeMenu',
        payload: {
          menuIds: menuCheckIds,
          employeeId: currentItem.employeeId
        },
        callBack:()=>{
          this.togglePowerMenuActing(false)
        }
      })
    }
  }

  getRoleMenu = (payload) => {
    debugger
    this.setState({
      currentItem: payload
    })
    this.setpowerMemuLoading();
    this.props.dispatch({
      type: 'powerModel/getRoleMenu',
      payload: {
        ...payload
      }
    })
    this.props.dispatch({
      type: 'powerModel/getElementPower',
      payload: {
        ...payload
      },
      currentItem: payload
    })
  }

  getPowerEmployee = (payload) => {

    this.setState({ currentItem: payload })
    this.setpowerMemuLoading();

    this.props.dispatch({
      type: 'powerModel/getEmployeeMenu',
      payload: {
        ...payload
      }
    })
    this.props.dispatch({
      type: 'powerModel/getEmployeeElementPower',
      payload: {
        ...payload
      }
    })
  }

  renderForm() {
    const { expandForm } = this.state;
    return this.renderSimpleForm();
  }

  render() {
    const { powerModel: { moduleList, menuList, menuCheckIds, powerMemuLoading, powerElementLoading ,
      powerMenuActing,powerElementActing}, loading } = this.props;
    const { currentItem, currentTab } = this.state;

    const header = ReturnHeader(currentItem.action);
    const OrgProps = {
      getEmployee: this.getOrgMenu,
      setCurrentOrg: this.setCurrentOrg,
      addEmployeeShow: this.addEmployeeShow
    }

    const RoleProps = {

      getEmployee: this.getRoleMenu,
      addEmployeeShow: this.addEmployeeShow
    }
    const EmployeeProps = {
      getPowerEmployee: this.getPowerEmployee
    }

    const powerDesignProps = {
      powerElementLoading,
      powerElementActing,
      currentTab,
      header,
      currentItem,
      moduleList,
      setModuleList: this.setModuleList,
      getAllModule: this.getAllModule,
      saveRelationElement: this.saveElementPower
    }

    const powerDesignMenuProps = {
      powerMenuActing,
      powerMemuLoading,
      currentTab,
      header,
      currentItem,
      menuList,
      menuCheckIds,
      setOrgMenu: this.setOrgMenu,
      getMenuTree: this.getMenuTree,
      saveRelationMenu: this.saveRelationMenu
    }
    return (<PageHeaderWrapper>
      <Card bordered={false} style={{height:winHeight}} bodyStyle={{ padding: '0 24px 24px' }}>
        <div className={styles.powerWrap}>
          <div className={styles.leftCon} style={{ borderRight: '1px solid #ddd' }}>
            <Tabs defaultActiveKey="org" onChange={e => this.setcurrentTab(e)}>
              <TabPane tab={<span>
                <Icon type='apartment'></Icon>
                机构
              </span>} key="org">
                <OrganizationCom {...OrgProps}></OrganizationCom>
              </TabPane>
              <TabPane tab={<span>
                <Icon type='idcard'></Icon>
                角色
              </span>} key="role">
                <RoleCom {...RoleProps}></RoleCom>
              </TabPane>
              <TabPane tab={<span>
                <Icon type='team'></Icon>
                人员
              </span>} key="employee">
                <EmployeeCom {...EmployeeProps}></EmployeeCom>
              </TabPane>
            </Tabs>

          </div>
          <div className={styles.rightCon} span={18}>
            <Tabs defaultActiveKey="1">
              <TabPane tab="模块" key="1">
                <PowerDesignCom {...powerDesignProps} />
              </TabPane>
              <TabPane tab="菜单" key="2">
                <PowerDesignMenuCom {...powerDesignMenuProps} />
              </TabPane>

            </Tabs>

          </div>
        </div>
      </Card>

    </PageHeaderWrapper>)
  }
}

export default Form.create()(PowerRouteCom);
