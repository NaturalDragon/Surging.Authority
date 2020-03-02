/* eslint-disable no-underscore-dangle */
/* eslint-disable react/react-in-jsx-scope */

import react, { Fragment } from 'react';
import { Icon, message, Popconfirm } from 'antd'

import { New, Modify, GetForModify, GetRoleTypeList ,Remove} from '@/services/role'
import { Guid } from '@/utils/com'
import AddCom from './Add'
import {authorAction} from '@/utils/constant'

import injectAuth from '@/components/injectAuth'
import { permissionJson } from '@/utils/permission'

const AuthorIcon=injectAuth(Icon)
const permission = permissionJson.structure

import styles from './Index.less'



class Index extends react.Component {

    state = {
        dataInfo: {
            roleType: 0,
            name: ''
        },
        roleTypeList: [],
        mode: 'add',// add or modify
        addVisible: false,
        actioning: false,
        modifyLoading: false,
        currentOrgItem: {},
    }

    componentDidMount() {
        this.init();
    }

    init = () => {
        const _this = this;
        GetRoleTypeList({}).then(result => {
            _this.setState({
                roleTypeList: result
            })
        })

    }

    onTypeMouseEnter = (item, flag) => {
        console.log('onTypeMouseEnter')
        const { roleTypeList } = this.state;
        roleTypeList.map(type => {
            type.selected = false;
            if (type.id === item.id) {
                type.selected = true
            }
            type.roleList.map(role => {
                role.selected = false;

            })

        })
        this.setState({
            roleTypeList
        })
    }

    onTypeMouseLevea = (item) => {
        console.log('onTypeMouseLevea')
        const { roleTypeList } = this.state;
        roleTypeList.map(type => {
            type.selected = false;

            type.roleList.map(role => {
                role.selected = false;

            })
        })
        this.setState({
            roleTypeList
        })
    }

     onRemove=(id)=>{
         message.loading('处理中...',0)
       Remove({id:id}).then(result=>{
           message.destroy()
           if(result.isValid){
            this.init()}
            else{
                message.error(result.errorMessages)
            }
       })
    }

    onRoleMouseEnter = (item) => {
        console.log('onRoleMouseEnter')
        const { roleTypeList } = this.state;
        roleTypeList.map(type => {
            //type.selected = false;
            type.roleList.map(role => {
                if (item.id === role.id) {
                    role.selected = true;
                }
            })
        })
        this.setState({
            roleTypeList
        })
    }

    onRoleMouseLevea = (item) => {
        console.log('onRoleMouseLevea')
        const { roleTypeList } = this.state;
        roleTypeList.map(type => {
            // type.selected = false;
            type.roleList.map(role => {
                role.selected = false;

            })
        })
        this.setState({
            roleTypeList
        })
    }


    addShow = item => {
        const { dataInfo } = this.state;
        debugger
        dataInfo.roleType = item.id
        this.setState({
            dataInfo
        })
        this.setState({
            mode: 'add',
            addVisible: true,
            currentTypeItem: item,
            dataInfo
        })
    }

    add = (obj) => {
        const { currentTypeItem } = this.state;
        const payload = { id: Guid(), ...obj }
        New(payload).then(res => {
            this.setState({ addVisible: false, actioning: false })
            this.init();
        })
    }

    addOk = (ele) => {

        this.setState({ actioning: true })
        if (this.state.mode === 'add') {

            this.add(ele);
        } else {
            Modify({ ...this.state.dataInfo, ...{ name: ele.name } }).then(res => {
                if (res.isValid) {
                    this.setState({
                        addVisible: false,
                        actioning: false,
                    })
                }
                this.init();
            });
        }
    }

    modifyShow = (item) => {
        const _this = this;
        this.setState({
            mode: 'modify',
            modifyLoading: true,
            addVisible: true,
            currentOrgItem: item
        })
        GetForModify({ id: item.id }).then(res => {
            _this.setState({
                dataInfo: res,
                modifyLoading: false
            })
        })
    }

    addCancel = () => {
        this.setState({ addVisible: false })
    }


    render() {
        const {operable}=this.props;
        const addProps = {
            dataInfo: this.state.dataInfo,
            modifyLoading: this.state.modifyLoading,
            addVisible: this.state.addVisible,
            actioning: this.state.actioning,
            addOk: this.addOk,
            addCancel: this.addCancel,
            roleTypeList: this.state.roleTypeList
        }
        return (<Fragment>
            <AddCom {...addProps} ></AddCom>
            {
                this.state.roleTypeList.map(type => <div


                    onMouseEnter={e => { this.onTypeMouseEnter(type); }}
                    onMouseLeave={e => { this.onTypeMouseLevea(type); }}
                    className={styles.role}>
                    <div className={styles.roleTypeItem}>
                        <span>{type.name}</span>
                        {
                           operable&& type.selected && <div className={styles.tool}><AuthorIcon
                            auth={permission.roleNew}
                                onClick={e => this.addShow(type)}
                                type="file-add" /></div>
                        }

                    </div>
                    <div className={styles.roleWrap}>
                        {type.roleList.map(role => <div onClick={
                            e => { this.props.getEmployee({ action:authorAction.role,name:role.name,roleId: role.id }) }
                        } className={styles.roleItem}
                            onMouseEnter={e => { this.onRoleMouseEnter(role); e.stopPropagation() }}
                            onMouseLeave={e => { this.onRoleMouseLevea(role); e.stopPropagation() }}
                        >
                            <span>{role.name}</span>
                            {
                                operable&&role.selected && <div className={styles.tools}>
                                    <AuthorIcon auth={permission.empNew} onClick={e => this.props.addEmployeeShow('',role)} className={styles.tool} type="user-add" />
                                    <AuthorIcon auth={permission.roleModify} onClick={e => { this.modifyShow(role); e.stopPropagation() }} className={styles.tool} type="edit" />
                                   <Popconfirm
                                    title="确定删除吗?"
                                    onConfirm={e=>{this.onRemove(role.id);e.stopPropagation()}}
                                    onCancel={e=>e.stopPropagation()}
                                    // onCancel={cancel}
                                    okText="确定"
                                    cancelText="取消"
                                   >
                                    <AuthorIcon auth={permission.roleRemove} onClick={e => {  e.stopPropagation() }} className={styles.tool} type="delete" />
                                    </Popconfirm>
                                </div>
                            }

                        </div>)}

                    </div>
                </div>)
            }

        </Fragment>)
    }

}

export default Index;