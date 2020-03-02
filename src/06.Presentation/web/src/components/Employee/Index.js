/* eslint-disable react/no-unused-state */
/* eslint-disable no-nested-ternary */
/* eslint-disable react/react-in-jsx-scope */
import { List, message, Avatar, Spin, Input } from 'antd';
import InfiniteScroll from 'react-infinite-scroller';
import _ from 'underscore'
import { GetListPagedOriginal } from '@/services/employee'
import styles from './Index.less'
import { authorAction } from '@/utils/constant'

// import './global.less'
// eslint-disable-next-line no-undef
let lazySearch = ''
export default class Employee extends React.Component {

    state = {
        data: [],
        queryKey: '',
        loading: false,
        hasMore: true,
        searchPage: {
            loading: false,
            hasMore: true
        },
        pagination: { pageIndex: 1, pageSize: 10 }
    };

    componentDidMount() {
        lazySearch = _.debounce(this.loadData, 300)

        this.loadData(true);
    }

    loadData = (isRefresh) => {
        const { pagination, data, queryKey,hasMore } = this.state;
        debugger
        const pageIndex = isRefresh ? pagination.pageIndex : pagination.pageIndex + 1
        if(!isRefresh&&!hasMore){
            return
        }
        this.setState({loading:true})
        GetListPagedOriginal({
            pageIndex,
            pageSize: pagination.pageSize,
            queryKey
        }).then(result => {

            const {data}=result
            const hasMoreSta=result.pageIndex*result.pageSize<=result.total

            if (isRefresh) {
                this.setState({
                    data: data,
                    pagination:{pageIndex:result.pageIndex,pageSize:result.pageSize,total:result.total},
                    hasMore:hasMoreSta,
                    loading:false
                })
            } else {
                this.setState({
                    data: data.concat(data),
                    pagination:{pageIndex:result.pageIndex,pageSize:result.pageSize,total:result.total},
                    hasMore:hasMoreSta,
                    loading:false
                })
            }

        })
    }

    handleInfiniteOnLoad = () => {
        this.loadData(false);
    };

    onChange = (e) => {
        const { pagination } = this.state;
        this.setState({
            queryKey: e,
            hasMore:false,
            pagination: { ...pagination, ...{ pageIndex: 1 } }
        })
        debugger
        this.scroll.pageLoaded = 0
    //    / this.scroll.props.pageStart=0;
        lazySearch(true);
    }

    render() {

        const { pagination,loading,hasMore, data } = this.state;
        return (
            <div>
                <Input.Search style={{ marginBottom: 4,width:'95%' }}
                    onChange={
                        e => { this.onChange(e.target.value) }
                    }
                    placeholder="姓名、手机号"></Input.Search>
                <div className={styles.demo_infinite_container} style={{ height: 400,width:'95%' }}>

                    <InfiniteScroll  ref={scroll => { this.scroll = scroll; }}
                        initialLoad={false}
                        pageStart={pagination.pageIndex - 1}
                        loadMore={this.handleInfiniteOnLoad}
                        hasMore={!loading && hasMore}
                        useWindow={false}
                    >
                        <List
                            dataSource={data}
                            renderItem={item => (
                                <List.Item key={item.id}
                                onClick={
                                    e => {
                                        this.props.getPowerEmployee({
                                            employeeId: item.id,
                                            name: item.name, action: authorAction.employee
                                        })
                                    }
                                } 
                                >
                                    
                                    <List.Item.Meta
                                    description={item.mobile}
                                        title={<a style={{ height: 26, display: 'block' }}
                                            className={`${item.disabled ? styles.searchDisabled : (item.selected ? styles.searchSelected : '')}`}
                                            
                                            >{item.name}</a>}

                                    />
                                    <div>{item.roleName}</div>

                                </List.Item>
                            )}
                        >
                            {loading && hasMore && (
                                <div className={styles.demo_loading_container}>
                                    <Spin />
                                </div>
                            )}
                        </List>

                    </InfiniteScroll>
                </div>
            </div>
        )
    }
}