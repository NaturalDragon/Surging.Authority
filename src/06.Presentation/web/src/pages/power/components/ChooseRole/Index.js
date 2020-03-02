/* eslint-disable no-param-reassign */
/* eslint-disable react/react-in-jsx-scope */

import react, { Fragment } from 'react';
import { Divider, Checkbox, Modal } from 'antd';
import { GetRoleTypeList } from '@/services/role'
import styles from './Index.less'


class Index extends react.Component {

    state = {
        roleTypeList: []
    }

    componentDidMount() {
        const {roleList}=this.props;
        let roleIds=[];
        roleList.forEach(e=>roleIds.push(e.id))
        GetRoleTypeList({}).then(res => {
            res.map(type => {
                type.checkedValues=[]
                type.roleList.map(role => {
                    role.label = role.name;
                    role.value = role.id;
                    if(roleIds.includes(role.id)){
                        type.checkedValues.push(role.id)
                    }
                })
            })
            debugger
            this.setState({
                roleTypeList: res
            })
        })
    }

    onChange=(type,checkedValue)=>{
            const {roleTypeList}=this.state;
            roleTypeList.forEach(item=>{
                if(type.id===item.id){
                    type.checkedValues=checkedValue
                }
            })
            this.setState({
                roleTypeList
            })

    }

    onOk=()=>{
        let list=[]
        const {roleTypeList}=this.state;
        roleTypeList.forEach(type=>{
            type.roleList.forEach(role=>{
                if(type.checkedValues.includes(role.id)){
                    list.push({name:role.name,id:role.id})
                }
            })
        })
        this.props.SetRoleList(list)
        this.props.RoleModalClose();
        // console.log(list)
    }

    render() {
        const { roleModalShow } = this.props
        return (<Modal
        width={700}
        title='编辑员工角色'
            visible={roleModalShow}

            onCancel={this.props.RoleModalClose}
            onOk={this.onOk}
        >
            {
                this.state.roleTypeList.map(type =>

                    <div className={styles.roleWrap}>
                        <div className={styles.roleTypeTitle}>{type.name}：</div>
                      
                        <Checkbox.Group onChange={e=>this.onChange(type,e)} 
                        options={type.roleList} value={type.checkedValues} />
                    </div>)
            }


            {/* <div style={{ padding: 20 }}>
                <div>资质</div>
                <Divider></Divider>
                <Checkbox.Group options={plainOptions} defaultValue={['Apple']} />
            </div> */}


        </Modal>)
    }
}

export default Index;