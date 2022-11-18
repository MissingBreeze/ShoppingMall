<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button type="primary" icon="plus" @click="hanldleAdd()">新建</a-button>
      <a-button
        type="primary"
        icon="minus"
        @click="handleDelete(selectedRowKeys)"
        :disabled="!hasSelected()"
        :loading="loading"
      >删除</a-button>
      <a-button type="primary" icon="redo" @click="getDataList()">刷新</a-button>
    </div>

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="10">
          <a-col :md="4" :sm="24">
            <a-form-item label="用户名">
              <a-input v-model="queryParam.userName" placeholder="用户名" />
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="24">
            <a-form-item label="订单状态">
              <a-select default-value="100" style="width: 120px" @change="handleChange">
                <a-select-option value="100">
                  全部
                </a-select-option>
                <a-select-option value="1">
                  待审核
                </a-select-option>
                <a-select-option value="2">
                  已通过
                </a-select-option>
                <a-select-option value="0">
                  已拒绝
                </a-select-option>
                <a-select-option value="3">
                  已撤消
                </a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="24">
            <a-button type="primary" @click="() => {this.pagination.current = 1; this.getDataList()}">查询</a-button>
            <a-button style="margin-left: 8px" @click="() => (queryParam = {})">重置</a-button>
          </a-col>
        </a-row>
      </a-form>
    </div>

    <a-table
      ref="table"
      :columns="columns"
      :rowKey="row => row.Id"
      :dataSource="data"
      :pagination="pagination"
      :loading="loading"
      @change="handleTableChange"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }"
      :bordered="true"
      size="small"
    >
      <span slot="action" slot-scope="text, record">
        <template>
          <a @click="handleChangeState(text, record.Id, 2)">通过</a>
          <a-divider type="vertical" />
          <a @click="handleChangeState(record.Id, 0)">拒绝</a>
          <a-divider type="vertical" />
          <a @click="handleChangeState(record.Id, 3)">撤消</a>
        </template>
      </span>
    </a-table>

  </a-card>
</template>

<script>

const columns = [
  { title: '用户名称', dataIndex: 'UserName', width: '10%' },
  { title: '金额', dataIndex: 'Amount', width: '10%' },
  { title: '类型', dataIndex: 'TypeName', width: '10%' },
  { title: '订单状态', dataIndex: 'StateName', width: '10%' },
  { title: '备注', dataIndex: 'Remark', width: '10%' },
  { title: '操作', dataIndex: 'action', scopedSlots: { customRender: 'action' } }
]

export default {
  components: {
  },
  mounted() {
    this.getDataList()
  },
  data() {
    return {
      data: [],
      pagination: {
        current: 1,
        pageSize: 10,
        showTotal: (total, range) => `总数:${total} 当前:${range[0]}-${range[1]}`
      },
      filters: {},
      sorter: { field: 'Id', order: 'asc' },
      loading: false,
      columns,
      queryParam: {},
      selectedRowKeys: []
    }
  },
  methods: {
    handleTableChange(pagination, filters, sorter) {
      this.pagination = { ...pagination }
      this.filters = { ...filters }
      this.sorter = { ...sorter }
      this.getDataList()
    },
    getDataList() {
      this.selectedRowKeys = []

      this.loading = true
      this.$http
        .post('/ShoppingMall/AccountOrder/BasicAccountOrder/GetDataList', {
          PageIndex: this.pagination.current,
          PageRows: this.pagination.pageSize,
          SortField: this.sorter.field || 'Id',
          SortType: this.sorter.order,
          Search: this.queryParam,
          ...this.filters
        })
        .then(resJson => {
          this.loading = false
          this.data = resJson.Data
          const pagination = { ...this.pagination }
          pagination.total = resJson.Total
          this.pagination = pagination
        })
    },
    onSelectChange(selectedRowKeys) {
      this.selectedRowKeys = selectedRowKeys
    },
    hasSelected() {
      return this.selectedRowKeys.length > 0
    },
    hanldleAdd() {
      this.$refs.editForm.openForm()
    },
    handleEdit(id) {
      this.$refs.editForm.openForm(id)
    },
    handleDelete(ids) {
      var thisObj = this
      this.$confirm({
        title: '确认删除吗?',
        onOk() {
          return new Promise((resolve, reject) => {
            thisObj.$http.post('/ShoppingMall/AccountOrder/BasicAccountOrder/DeleteData', ids).then(resJson => {
              resolve()

              if (resJson.Success) {
                thisObj.$message.success('操作成功!')

                thisObj.getDataList()
              } else {
                thisObj.$message.error(resJson.Msg)
              }
            })
          })
        }
      })
    },
    handleChangeState(id, state) {
      var thisObj = this
      var title = ''
      if (state == 2) {
        title = '确认通过吗?'
      } else if (state == 0) {
        title = '确认拒绝吗?'
      } else if (state == 3) {
        title = '确认撤消吗?'
      }
      this.$confirm({
        title: title,
        onOk() {
          return new Promise((resolve, reject) => {
            thisObj.$http.get('/ShoppingMall/AccountOrder/BasicAccountOrder/ChangeState?id=' + id + '&state=' + state).then(resJson => {
              resolve()
              if (resJson.Success) {
                thisObj.$message.success('操作成功!')
                thisObj.getDataList()
              } else {
                thisObj.$message.error(resJson.Msg)
              }
            })
          })
        }
      })
    },
    handleChange(value) {
      if (value != 100) {
        this.queryParam.state = value
      } else {
        this.queryParam.state = null
      }
    },
  }
}
</script>