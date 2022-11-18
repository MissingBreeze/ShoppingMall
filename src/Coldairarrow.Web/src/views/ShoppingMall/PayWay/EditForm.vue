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
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout" :validateOnRuleChange="true">
        <a-form-model-item label="名称" prop="Name">
          <a-input v-model="entity.Name" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="类型" prop="Type">
          <a-radio-group name="radioGroup" v-model="entity.Type" @change="radioChange" :default-value="'1'">
            <a-radio :value="'1'">
              银行卡
            </a-radio>
            <a-radio :value="'3'">
              二维码
            </a-radio>
            <a-radio :value="'2'">
              链接
            </a-radio>
          </a-radio-group>
        </a-form-model-item>
        <span v-show="entity.Type == '1'">
          <a-form-model-item label="银行名称" prop="BankName">
            <a-input v-model="entity.BankName" autocomplete="off" />
          </a-form-model-item>
          <a-form-model-item label="开户行" prop="OpenBank">
            <a-input v-model="entity.OpenBank" autocomplete="off" />
          </a-form-model-item>
          <a-form-model-item label="卡号" prop="BankNo">
            <a-input v-model="entity.BankNo" autocomplete="off" />
          </a-form-model-item>
          <a-form-model-item label="开户人" prop="UserName">
            <a-input v-model="entity.UserName" autocomplete="off" />
          </a-form-model-item>
        </span>
        <span v-show="entity.Type == '3'">
          <a-form-model-item label="二维码" prop="QRPictures">
            <c-upload-img v-model="entity.QRPictures" :maxCount="5"></c-upload-img>
          </a-form-model-item>
        </span>
        <span v-show="entity.Type == '2'">
          <a-form-model-item label="链接" prop="Link">
            <a-input v-model="entity.Link" autocomplete="off" />
          </a-form-model-item>
        </span>
        <a-form-model-item label="说明" prop="Direction">
          <a-input v-model="entity.Direction" autocomplete="off" />
        </a-form-model-item>

      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
import CUploadImg from '@/components/CUploadImg/CUploadImg'
export default {
  props: {
    parentObj: Object
  },
  components: {
    CUploadImg
  },
  data() {
    return {
      layout: {
        labelCol: { span: 5 },
        wrapperCol: { span: 18 }
      },
      visible: false,
      loading: false,
      entity: { QRPictures: [] },
      rules: {
      },
      title: ''
    }
  },
  methods: {
    init() {
      this.visible = true
      this.entity = { QRPictures: [] }
      this.$nextTick(() => {
        this.$refs['form'].clearValidate()
      })
    },
    openForm(id, title) {
      this.init()

      if (id) {
        this.loading = true
        this.$http.post('/ShoppingMall/PayWay/BasicPay/GetTheData', { id: id }).then(resJson => {
          this.loading = false

          this.entity = resJson.Data
          if (!this.entity.QRPictures) {
            this.entity.QRPictures = []
          }
        })
      } else {
        this.entity.Type = '1'
      }
    },
    handleSubmit() {
      this.$refs['form'].validate(valid => {
        if (!valid) {
          return
        }
        this.loading = true
        this.$http.post('/ShoppingMall/PayWay/BasicPay/SaveData', this.entity).then(resJson => {
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
    },
    radioChange(e) {
      if (e.target.value == '1') {
        this.rules = {
          BankNo: [{ required: true, message: '必填' }],
          Name: [{ required: true, message: '必填' }],
        }
      } else if (e.target.value == '2') {
        this.rules = {
          Link: [{ required: true, message: '必填' }],
          Name: [{ required: true, message: '必填' }],
        }
      } else if (e.target.value == '3') {
        this.rules = {
          QRPictures: [{ required: true, message: '必填' }],
          Name: [{ required: true, message: '必填' }],
        }
      }
    }
  }
}
</script>
