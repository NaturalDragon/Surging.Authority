/* eslint-disable no-return-assign */
/* eslint-disable react/react-in-jsx-scope */
import { Divider, Checkbox, Breadcrumb, Spin, Button } from 'antd';
import react, { Fragment } from 'react';
import styles from './Index.less';
import injectAuth from '@/components/injectAuth'
import { permissionJson } from '@/utils/permission'

const AuthorButton = injectAuth(Button)
const permission = permissionJson.power

class Index extends react.Component {

    componentDidMount() {
       // this.props.getAllModule();
    }

    allOnChange=(mod,checked)=>{
        const { moduleList } = this.props;
        moduleList.forEach(m => {
            if (m.id === mod.id) {
               
                if(checked){
                    m.moduleElementActionRequests.forEach(element=>{
                        // eslint-disable-next-line no-continue
                        if(!element.disabled){
                        m.checkedValues.push(element.id)
                        }
                    })
                }else{
                    let c=[];
                    m.moduleElementActionRequests.forEach(element=>{
                        debugger
                        // eslint-disable-next-line no-continue
                        if(element.disabled){
                          c=c.concat(m.checkedValues.filter(e=>e===element.id))
                        }
                    })
                    m.checkedValues=c;
                }
            
            }
        })
        this.props.setModuleList(moduleList)
    }

    onChange = (mod, ids) => {
        const { moduleList } = this.props;
        
        moduleList.forEach(m => {
            if (m.id === mod.id) {
                m.checkedValues = ids;
            }
        })
        this.props.setModuleList(moduleList)
    }

    save = () => {
        const { moduleList } = this.props;
        let checkedValues = [];
        moduleList.forEach(e => checkedValues = checkedValues.concat(e.checkedValues));
        this.props.saveRelationElement(checkedValues)
    }

    render() {
        const { moduleList, header, currentItem, powerElementLoading,powerElementActing } = this.props;
        return (
            <Spin spinning={powerElementLoading}>
                <div className={styles.pageWrap}>
                    <div className={styles.header}>
                        <div className={styles.leftHeader}>
                            <Breadcrumb>
                                <Breadcrumb.Item>{header}</Breadcrumb.Item>
                                <Breadcrumb.Item>
                                    {currentItem.name}
                                </Breadcrumb.Item>
                            </Breadcrumb>
                        </div>
                        <AuthorButton auth={[permission.saveOrgModule,permission.saveRoleModule,permission.saveEmpModule]}  loading={powerElementActing} type='primary' onClick={this.save}>保存</AuthorButton>
                    </div>
                    <div className={styles.contentWrap}>
                        {
                            moduleList && moduleList.map(mod =>
                                <div className={styles.roleWrap}>
                                    <div className={styles.roleTypeTitle}>{mod.name}：
                            <Checkbox onChange={e=>this.allOnChange(mod,e.target.checked)}>全选</Checkbox>
                                    </div>
                                    {/* <Divider></Divider> */}

                                    <Checkbox.Group onChange={e => { this.onChange(mod, e) }}
                                        options={mod.moduleElementActionRequests} value={mod.checkedValues} />
                                    <Divider dashed></Divider>
                                </div>)
                        }
                    </div>

                </div>
            </Spin>
        )
    }

}

export default Index;