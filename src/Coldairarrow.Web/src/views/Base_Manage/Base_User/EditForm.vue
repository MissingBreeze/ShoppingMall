<template>
  <a-modal
    title="编辑表单"
    width="40%"
    :visible="visible"
    :confirmLoading="confirmLoading"
    @ok="handleSubmit"
    @cancel="()=>{this.visible=false}"
  >
    <a-spin :spinning="confirmLoading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-form-model-item label="用户名" prop="UserName">
          <a-input v-model="entity.UserName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="密码" prop="newPwd">
          <a-input v-model="entity.newPwd" type="password" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="姓名" prop="RealName">
          <a-input v-model="entity.RealName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="上级" prop="ParentId">
          <a-select
            v-model="entity.ParentName"
            placeholder="搜索用户账号或姓名"
            show-search
            :show-arrow="false"
            :filter-option="false"
            not-found-content="未查询到数据"
            @focus="focusUser"
            @search="searchUser(arguments[0], 0)"
            @change="(value)=>{entity.ParentId = value}"
            allowClear>
            <a-select-option v-for="d in userData" :key="d.Id" :code="d.UserName">
              {{ d.UserName }}  - {{ d.RoleName }}
            </a-select-option>
          </a-select>
        </a-form-model-item>
        <a-form-model-item label="归属客服" prop="ParentId">
          <a-select
            v-model="entity.BelongName"
            placeholder="搜索用户账号或姓名"
            show-search
            :show-arrow="false"
            :filter-option="false"
            not-found-content="未查询到数据"
            @focus="focusUser"
            @search="searchUser(arguments[0], 2)"
            @change="(value)=>{entity.BelongId = value}"
            allowClear>
            <a-select-option v-for="d in userData" :key="d.Id" :code="d.UserName">
              {{ d.UserName }} - {{ d.RoleName }}
            </a-select-option>
          </a-select>
        </a-form-model-item>
        <a-form-model-item label="角色" prop="RoleIdList">
          <a-select v-model="entity.RoleId">
            <a-select-option v-for="item in RoleOptionList" :key="item.Id">{{ item.RoleName }}</a-select-option>
          </a-select>
        </a-form-model-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>

export default {
  props: {
    afterSubmit: {
      type: Function,
      default: null
    }
  },
  data() {
    return {
      layout: {
        labelCol: { span: 5 },
        wrapperCol: { span: 18 }
      },
      visible: false,
      confirmLoading: false,
      userSearchLoading: false,
      entity: {},
      DepartmentIdTreeData: [],
      RoleOptionList: [],
      rules: {
        UserName: [{ required: true, message: '必填' }],
        RealName: [{ required: true, message: '必填' }],
      },
      userData: [],
    }
  },
  methods: {
    init() {
      this.visible = true
      this.entity = {}
      this.$nextTick(() => {
        this.$refs['form'].clearValidate()
      })
      this.$http.post('/Base_Manage/Base_Department/GetTreeDataList', {}).then(resJson => {
        if (resJson.Success) {
          this.DepartmentIdTreeData = resJson.Data
        }
      })
      this.$http.post('/Base_Manage/Base_Role/GetDataList', {}).then(resJson => {
        if (resJson.Success) {
          this.RoleOptionList = resJson.Data
        }
      })
    },
    openForm(id) {
      this.init()

      if (id) {
        this.$http.post('/Base_Manage/Base_User/GetTheData', { id: id }).then(resJson => {
          this.entity = resJson.Data
        })
      }
    },
    handleSubmit() {
      this.$refs['form'].validate(valid => {
        if (!valid) {
          return
        }
        this.confirmLoading = true
        this.$http.post('/Base_Manage/Base_User/SaveData', this.entity).then(resJson => {
          this.confirmLoading = false

          if (resJson.Success) {
            this.$message.success('操作成功!')
            this.afterSubmit()
            this.visible = false
          } else {
            this.$message.error(resJson.Msg)
          }
        })
      })
    },
    focusUser() {
      this.userData = []
    },
    searchUser(e, roleType) {
      if (e.length > 0) {
        this.$http.post('/Base_Manage/Base_User/GetDataList', {
          PageIndex: 1,
          PageRows: 10,
          Search: { keyword: e, roleType: roleType }
        }).then(resJson => {
          if (resJson.Success) {
            this.userData = resJson.Data
          }
        })
      }
    },
  }
}
</script>
