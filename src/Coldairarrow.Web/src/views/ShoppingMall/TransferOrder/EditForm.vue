<template>
  <a-modal
    :title="title"
    width="40%"
    :visible="visible"
    :confirmLoading="loading"
    @ok="handleSubmit"
    @cancel="()=>{this.visible=false}"
  >
    <a-spin :spinning="loading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-form-model-item label="类型。1转入，2转出" prop="Type">
          <a-input v-model="entity.Type" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="金额" prop="Amount">
          <a-input v-model="entity.Amount" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="银行名" prop="BankName">
          <a-input v-model="entity.BankName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="开户行" prop="OpenBank">
          <a-input v-model="entity.OpenBank" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="开户名" prop="OpenName">
          <a-input v-model="entity.OpenName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="卡号" prop="BankNo">
          <a-input v-model="entity.BankNo" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="二维码地址" prop="QRCodePath">
          <a-input v-model="entity.QRCodePath" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="链接" prop="Link">
          <a-input v-model="entity.Link" autocomplete="off" />
        </a-form-model-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
export default {
  props: {
    parentObj: Object
  },
  data() {
    return {
      layout: {
        labelCol: { span: 5 },
        wrapperCol: { span: 18 }
      },
      visible: false,
      loading: false,
      entity: {},
      rules: {},
      title: ''
    }
  },
  methods: {
    init() {
      this.visible = true
      this.entity = {}
      this.$nextTick(() => {
        this.$refs['form'].clearValidate()
      })
    },
    openForm(id, title) {
      this.init()

      if (id) {
        this.loading = true
        this.$http.post('/ShoppingMall/TransferOrder/BasicTransferOrder/GetTheData', { id: id }).then(resJson => {
          this.loading = false

          this.entity = resJson.Data
        })
      }
    },
    handleSubmit() {
      this.$refs['form'].validate(valid => {
        if (!valid) {
          return
        }
        this.loading = true
        this.$http.post('/ShoppingMall/TransferOrder/BasicTransferOrder/SaveData', this.entity).then(resJson => {
          this.loading = false

          if (resJson.Success) {
            this.$message.success('操作成功!')
            this.visible = false

            this.parentObj.getDataList()
          } else {
            this.$message.error(resJson.Msg)
          }
        })
      })
    }
  }
}
</script>
