/* eslint-disable no-nested-ternary */
/* eslint-disable react/react-in-jsx-scope */
import react, { Fragment } from 'react';
import { Tree, Icon, Button, Dropdown, Menu, Modal, Input, Empty ,Spin, message} from 'antd';
import _ from 'underscore';
import { New, GetForModify, Modify, GetOrganizationByParentId,Remove } from '@/services/organization'
import { Guid, LoadTree } from '@/utils/com'
import { authorAction } from '@/utils/constant'
import AddCom from './Add'
import injectAuth from '@/components/injectAuth'
import { permissionJson } from '@/utils/permission'


const AuthorMenuItem=injectAuth(Menu.Item)
const permission = permissionJson.structure
const { TreeNode } = Tree;
let lazyLoad;
function LoadModifyTree(treeData, id, nowTree) {

    treeData.forEach(ele => {
        if (ele.id === id) {
            ele.title = nowTree.name;
        }
        if (ele.children) {
            return LoadModifyTree(ele.children, id, nowTree);
        }
    })
    return treeData;
}


const TitleCom = ({ item, addShow, addEmployeeShow, modifyShow,remove, operable }) => {
    const content = (
        <Menu>
            <AuthorMenuItem auth={permission.orgNew} onClick={e => addShow(item)}>添加子部门</AuthorMenuItem>
            <AuthorMenuItem auth={permission.empNew} onClick={e => addEmployeeShow(item)}>新增成员</AuthorMenuItem>
            <AuthorMenuItem auth={permission.orgModify} onClick={e => modifyShow(item)}>修改</AuthorMenuItem>
            <AuthorMenuItem auth={permission.orgRemove} onClick={e=>remove(item)}>删除</AuthorMenuItem>
        </Menu>
    );
    return (<div>
        <span style={{ display: 'inline-block', width: 'calc(100% - 24px)' }} >{item.title}</span>
        <Dropdown trigger='click' overlay={content} placement='bottomCenter'>
            <Icon type='more' onClick={e => e.stopPropagation()} />
        </Dropdown>
    </div>);
}
class Organization extends react.Component {

    state = {
        dataInfo: { name: '' },
        mode: 'add',//add or modify
        addVisible: false,
        modifyLoading: false,
        currentOrgItem: {},
        treeData: [],
        isSearch: false,
        orgLoading:false,
    };

    // eslint-disable-next-line no-useless-constructor
    constructor(props) {

     
        super(props);
    }

    componentDidMount() {
        lazyLoad = _.debounce(this.search, 500)

        this.initOrg()
    }

    initOrg = (id, queryKey) => {
        const _this = this;
        const { treeData } = this.state;
        if(!id){
            this.setState({orgLoading:true})
        }
        GetOrganizationByParentId({
            parentId: id,
            type: 0,
            queryKey
        }).then(data => {
            let childList = []
            debugger
            data.forEach(ele => {
                childList.push({
                    title: ele.name, key: ele.id, id: ele.id,
                    parentId: ele.parentId, name: ele.name
                })
            })
            let newTreeData = treeData.length > 0 ? treeData : childList
            const nodes = LoadTree(newTreeData, id, childList)
            _this.setState({
                treeData: nodes,//toNodeProps(res),
            });

            if (!id) {
                this.setState({orgLoading:false})
                const orgEle = data[0];
                if (orgEle) {
                    const orgItem = { action: authorAction.organization, name: orgEle.name, id: orgEle.id }
                    this.props.setCurrentOrg(orgItem)
                    this.props.getEmployee({ organizationId: orgEle.id })
                }
            }
        })
    }

    search = (value) => {
        this.setState({
            treeData: [],

        }, () => {
            this.initOrg('', value)
        })

    }

    onChange = (value) => {

        this.setState({
            isSearch: !!value
        })
        lazyLoad(value)

    }

    addShow = item => {
        this.setState({
            mode: 'add',
            addVisible: true,
            currentOrgItem: item
        })
    }

    addTopShow = () => {
        this.addShow({ key: '00000000-0000-0000-0000-000000000000' })
    }

    add = (name, parentId) => {
        const _this = this;
        const { currentOrgItem } = this.state;
        const payload = {
            id: Guid(), departmentId: 1, order: 1, OrganizationNatureId: '0', name: name,
            parentId: parentId
        }
        New(payload).then(res => {
            this.setState({ addVisible: false })
            _this.initOrg(currentOrgItem.key)
        })
    }

    addOk = (ele) => {

        if (this.state.mode === 'add') {
            this.add(ele.name, this.state.currentOrgItem.key);
        } else {
            const _this = this;
            const { currentOrgItem, treeData } = this.state;
            debugger
            Modify({ ...this.state.dataInfo, ...{ name: ele.name } }).then(res => {
                if (res.isValid) {
                    const newData = LoadModifyTree(treeData, currentOrgItem.id, { name: ele.name })
                    this.setState({
                        addVisible: false,
                        treeData: newData
                    })
                }
                // _this.initOrg(currentOrgItem.parentId)
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
        GetForModify({ id: item.key }).then(res => {
            _this.setState({
                dataInfo: res,
                modifyLoading: false
            })
        })
    }

    onRemove=(item)=>{
        console.log(item)
        // return
        message.loading('处理中...')
        Remove({id:item.key}).then(result=>{
            message.destroy()
            if(result.isValid){
             this.initOrg(item.parentId)}
             else{
                 message.error(result.errorMessages)
             }
        })
    }

    addCancel = () => {
        this.setState({ addVisible: false })
    }

    onLoadData = (treeNode) => {
        const item = treeNode.props.dataRef;
        // eslint-disable-next-line no-underscore-dangle
        const _this = this;
        return new Promise((resolve) => {
            if (treeNode.props.children) {
                resolve();
                return;
            }
            this.initOrg(item.key)
            setTimeout(() => {
                resolve();
            }, 500);
        });
    }
    
    renderTreeNodes = (data) => {
        const titleProps = {
            modifyShow: this.modifyShow,
            addShow: this.addShow,
            addEmployeeShow: this.props.addEmployeeShow,
            remove:this.onRemove

        }
        return data.map(item => {
            if (item.children) {
                return (
                    <TreeNode icon={<Icon type="apartment" />} title={<TitleCom {...titleProps} item={item} />} key={item.key} dataRef={item}>
                        {this.renderTreeNodes(item.children)}
                    </TreeNode>
                );
            }
            return <TreeNode  {...item} icon={<Icon type="apartment" />} title={<TitleCom {...titleProps}
                item={item} />} dataRef={item} />;
        });
    }

    renderTreeNodesNoOperable = (data) => {
        
        return data.map(item => {
            if (item.children) {
                return (
                    <TreeNode icon={<Icon type="apartment" />} title={item.title} key={item.key} dataRef={item}>
                        {this.renderTreeNodesNoOperable(item.children)}
                    </TreeNode>
                );
            }
            return <TreeNode  {...item} icon={<Icon type="apartment" />} title={item.title} dataRef={item} />;
        });
    }

    onSelect = (selectedKeys, e) => {
        const orgItem = { action: authorAction.organization, id: selectedKeys[0], name: e.node.props.dataRef.title };
        this.props.setCurrentOrg(orgItem)
        this.props.getEmployee({ organizationId: selectedKeys[0] })
    }

    render() {
        const { isSearch, treeData,orgLoading } = this.state;
        const {operable}=this.props
        const AddProps = {
            dataInfo: this.state.dataInfo,
            modifyLoading: this.state.modifyLoading,
            addVisible: this.state.addVisible,
            addOk: this.addOk,
            addCancel: this.addCancel
        }
        return (<Fragment>
            <AddCom {...AddProps} ></AddCom>
            <Input.Search placeholder='机构名称' style={{ width: '95%' }} onChange={e => {
                this.onChange(e.target.value)
            }}></Input.Search>

            <Spin spinning={orgLoading}>
            {
                treeData.length <= 0 ?
                    (!isSearch ?
                        <div style={{margin:'40px auto 0',width:88}}><Button onClick={e => {
                            this.addTopShow()
                            // this.add('思源时代', '00000000-0000-0000-0000-000000000000')
                        }}>新增机构</Button></div> : <Empty image={Empty.PRESENTED_IMAGE_SIMPLE} />)

                    :
                    <Tree
                    style={{height:400,overflow:'auto'}}
                        showIcon
                        loadData={this.onLoadData}
                        onSelect={this.onSelect}
                        selectedKeys={[]}

                    >{
                        operable?this.renderTreeNodes(treeData)
                        :this.renderTreeNodesNoOperable(treeData)
                    }

                    </Tree>
            }
            </Spin>
        </Fragment>)
    }
}
export default Organization