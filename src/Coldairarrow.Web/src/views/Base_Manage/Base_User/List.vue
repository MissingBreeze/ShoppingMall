<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button v-if="hasPerm('Base_User.Add')" type="primary" icon="plus" @click="hanldleAdd()">新建</a-button>
      <a-button
        v-if="hasPerm('Base_User.Delete')"
        type="primary"
        icon="minus"
        @click="handleDelete(selectedRowKeys)"
        :disabled="!hasSelected()"
        :loading="loading"
      >删除</a-button>
    </div>

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="48">
          <a-col :md="6" :sm="24">
            <a-form-item label="用户名">
              <a-input v-model="queryParam.keyword" placeholder />
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="24">
            <a-form-item label="角色">
              <a-select default-value="100" style="width: 120px" @change="handleChange">
                <a-select-option value="100">
                  全部
                </a-select-option>
                <a-select-option value="2">
                  管理员
                </a-select-option>
                <a-select-option value="3">
                  客服
                </a-select-option>
                <a-select-option value="4">
                  用户
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
          <template v-if="hasPerm('Base_User.Edit')">
            <a @click="handleEdit(record.Id)">编辑</a>
            <a-divider type="vertical" />
            <a @click="handleAccount(record.Id, record.Balance)">资金</a>
            <a-divider type="vertical" />
          </template>
          <a v-if="hasPerm('Base_User.Delete')" @click="handleDelete([record.Id])">删除</a>
        </template>
      </span>
    </a-table>

    <edit-form ref="editForm" :afterSubmit="getDataList"></edit-form>
    <account ref="account" :afterSubmit="getDataList"></account>
  </a-card>
</template>

<script>
import EditForm from './EditForm'
import Account from './Account'

const columns = [
  { title: '用户名', dataIndex: 'UserName', width: '10%' },
  { title: '姓名', dataIndex: 'RealName', width: '10%' },
  { title: '上级', dataIndex: 'ParentName', width: '10%' },
  { title: '所属客服', dataIndex: 'BelongName', width: '10%' },
  { title: '所属角色', dataIndex: 'RoleName', width: '10%' },
  { title: '操作', dataIndex: 'action', scopedSlots: { customRender: 'action' } }
]

export default {
  components: {
    EditForm,
    Account,
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
      visible: false,
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
        .post('/Base_Manage/Base_User/GetDataList', {
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
            thisObj.submitDelete(ids, resolve, reject)
          }).catch(() => console.log('Oops errors!'))
        }
      })
    },
    submitDelete(ids, resolve, reject) {
      this.$http.post('/Base_Manage/Base_User/DeleteData', ids).then(resJson => {
        resolve()

        if (resJson.Success) {
          this.$message.success('操作成功!')

          this.getDataList()
        } else {
          this.$message.error(resJson.Msg)
        }
      })
    },
    handleAccount(id, balance) {
      this.$refs.account.openForm({ UserId: id, Balance: balance })
    },
    handleChange(value) {
      if (value != 100) {
        this.queryParam.roleType = value
      } else {
        this.queryParam.roleType = null
      }
    }
  }
}
</script>