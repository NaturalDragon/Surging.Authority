/* eslint-disable react/react-in-jsx-scope */

import react from 'react';
import { Input, Tree, Modal, Icon, List, Row, Col, Tabs } from 'antd';
import styles from './Index.less'
import { GetOrganizationByParentId } from '@/services/organization'
import { LoadTree } from '@/utils/com'

const { Search } = Input;

const { TreeNode } = Tree;
const { TabPane } = Tabs;
class Index extends react.Component {
    state = {
        treeData: [],
        choosedData: []
    }

    componentDidMount() {
        this.initOrg()
        const { choosedOrgList } = this.props
        this.setState({
            choosedData: choosedOrgList.filter(e => { return { ...e } })
        })
    }

    initOrg = (id) => {
        const _this = this;
        const { treeData } = this.state;
        GetOrganizationByParentId({
            parentId: id,
            type: 0
        }).then(data => {
            let childList = []
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
        })
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

        }
        return data.map(item => {
            if (item.children) {
                return (
                    <TreeNode icon={<Icon type="apartment" />} title={item.title} key={item.key} dataRef={item}>
                        {this.renderTreeNodes(item.children)}
                    </TreeNode>
                );
            }
            return <TreeNode  {...item} icon={<Icon type="apartment" />} title={item.title} dataRef={item} />;
        });
    }

    onSelect = (ids, nodes) => {
        const selected = nodes.selected;
        const node = nodes.node.props.dataRef;
        let { choosedData } = this.state;
        debugger
        if (selected) {
            choosedData.push({ name: node.title, id: node.id })
        } else {
            choosedData = choosedData.filter(e => e.id !== node.id);
        }

        this.setState({
            choosedData
        })

    }

    RemoveKeys = (item) => {
        let { choosedData } = this.state;
        choosedData = choosedData.filter(e => e.id !== item.id);
        this.setState({
            choosedData
        })
    }

    OnOk = () => {
        this.props.OrgModalClose();
        this.props.OrgModalOk(this.state.choosedData);
    }

    render() {
        const { orgMoadlShow } = this.props;
        const { choosedData } = this.state;
        const orgIds = [];
        choosedData.forEach(e => { orgIds.push(e.id) })
        return (
            <Modal
                onCancel={this.props.OrgModalClose}
                onOk={this.OnOk}
                width={800} title='选择机构' visible={orgMoadlShow}>

                <Row>
                    <Col span={12} style={{ borderRight: '1px solid #ddd', paddingRight: 15 }}>

                        <div className={styles.searchWrap}>
                            <Search style={{ marginBottom: 8 }} placeholder="请输入机构名称"></Search>
                        </div>
                        <div style={{ width: '100%', overflow: 'hidden', height: 350 }}>
                            <Tabs defaultActiveKey="1">
                                <TabPane tab="组织机构" key="1">
                                    <Tree
                                        multiple
                                        selectedKeys={orgIds}
                                        showIcon
                                        loadData={this.onLoadData}
                                        onSelect={this.onSelect}

                                    >{this.renderTreeNodes(this.state.treeData)}

                                    </Tree>

                                </TabPane>
                            </Tabs>
                        </div>
                    </Col>
                    <Col span={12}>
                        <div style={{ width: '100%', paddingLeft: 15, }}>
                            <List
                                style={{ height: 380, overflow: 'auto' }}
                                header={<div >已选机构</div>}
                                bordered
                                dataSource={choosedData}
                                renderItem={item => (
                                    <List.Item>
                                        <div className={styles.orgItem}>
                                            <span className={styles.orgItemText}> {item.name}</span>
                                            <Icon
                                                onClick={e => { this.RemoveKeys(item) }}
                                                className={styles.orgItemIcon} type="close" />
                                        </div>
                                    </List.Item>
                                )}
                            />
                        </div>
                    </Col>
                </Row>
            </Modal>)
    }
}

export default Index;