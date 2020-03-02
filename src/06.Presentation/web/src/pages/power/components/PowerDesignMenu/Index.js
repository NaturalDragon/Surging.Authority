/* eslint-disable react/react-in-jsx-scope */
import { Tree, Checkbox, Modal, Icon, Button, Affix, Breadcrumb, Spin } from 'antd';
import react, { Fragment } from 'react';
import styles from './Index.less';
import injectAuth from '@/components/injectAuth'
import { permissionJson } from '@/utils/permission'

const AuthorButton = injectAuth(Button)
const permission = permissionJson.power
const { TreeNode } = Tree;

class Index extends react.Component {

  state = {
    expandedKeys: ['0-0-0', '0-0-1'],
    autoExpandParent: true,
    checkedKeys: ['0-0-0'],
    selectedKeys: [],
  };

  componentDidMount() {
    //this.props.getMenuTree();
  }

  onExpand = expandedKeys => {
    console.log('onExpand', expandedKeys);
    // if not set autoExpandParent to false, if children expanded, parent can not collapse.
    // or, you can remove all expanded children keys.
    this.setState({
      expandedKeys,
      autoExpandParent: false,
    });
  };

  onCheck = (checkedKeys, e) => {
    debugger
    this.props.setOrgMenu(checkedKeys.checked)
  };



  renderTreeNodes = data =>
    data.map(item => {
      if (item.children) {
        return (
          <TreeNode icon={<Icon type='menu' />} disableCheckbox={item.disabled} title={item.title} key={item.key} dataRef={item} >
            {this.renderTreeNodes(item.children)}
          </TreeNode>
        );
      }
      return <TreeNode icon={<Icon type='menu' />} disableCheckbox={item.disabled} key={item.key} {...item} />;
    });



  render() {
    const { powerMemuLoading, powerMenuActing, menuList, menuCheckIds, currentItem, header } = this.props;
    debugger
    return (
      <Spin spinning={powerMemuLoading} >
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
            <AuthorButton auth={[permission.saveOrgMenu,permission.saveRoleMenu,permission.saveEmployeeMenu]} loading={powerMenuActing} type='primary' onClick={this.props.saveRelationMenu}>保存</AuthorButton>
          </div>
          <div className={styles.treeWrap}>
            <Tree
              checkStrictly
              showIcon
              defaultExpandAll
              checkable
              // onExpand={this.onExpand}
              // expandedKeys={this.state.expandedKeys}
              // autoExpandParent={this.state.autoExpandParent}
              onCheck={this.onCheck}
              checkedKeys={menuCheckIds}
            //onSelect={this.onSelect}
            // selectedKeys={this.state.selectedKeys}
            >
              {this.renderTreeNodes(menuList)}
            </Tree>
          </div>
        </div>
      </Spin>
    )
  }

}

export default Index;