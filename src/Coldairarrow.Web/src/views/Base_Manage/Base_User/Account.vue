<template>
  <a-modal
    title="编辑账户"
    width="40%"
    :visible="visible"
    :confirmLoading="confirmLoading"
    @ok="handleSubmit"
    @cancel="()=>{this.visible=false}"
  >
    <a-spin :spinning="confirmLoading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-form-model-item label="类型" prop="Type">
          <a-radio-group name="radioGroup" v-model="entity.Type" @change="radioChange" :default-value="1">
            <a-radio :value="1">
              充值
            </a-radio>
            <a-radio :value="2">
              提现
            </a-radio>
          </a-radio-group>
        </a-form-model-item>
        <a-form-model-item label="当前余额" prop="Balance">
          <a-input v-model="entity.Balance" autocomplete="off" disabled/>
        </a-form-model-item>
        <a-form-model-item :label="labelName" prop="Amount">
          <a-input-number v-model="entity.Amount" :precision="2" class="inputCss"/>
        </a-form-model-item>
        <a-form-model-item label="备注" prop="Remark">
          <a-textarea v-model="entity.Remark" :rows="4" />
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
      labelName: '充值金额',
      entity: {},
      rules: {
        Amount: [{ required: true, message: '必填' }],
      },
    }
  },
  methods: {
    init() {
      this.visible = true
      // this.entity = {}
      this.$nextTick(() => {
        this.$refs['form'].clearValidate()
      })
    },
    openForm(data) {
      this.init()
      this.entity = data
      this.entity.Type = 1
    },
    handleSubmit() {
      this.$refs['form'].validate(valid => {
        if (!valid) {
          return
        }
        this.confirmLoading = true
        this.$http.post('/ShoppingMall/AccountOrder/BasicAccountOrder/SaveData', this.entity).then(resJson => {
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
    radioChange(e) {
      if (e.target.value == 1) {
        this.labelName = '充值金额'
      } else if (e.target.value == 2) {
        this.labelName = '提现金额'
      }
    }
  }
}
</script>
<style scoped>
.inputCss{
  width: 100%;
}
</style>
