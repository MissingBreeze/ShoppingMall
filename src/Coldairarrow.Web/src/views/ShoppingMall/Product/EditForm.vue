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
        <a-form-model-item label="产品名" prop="ProductName">
          <a-input v-model="entity.ProductName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="昵称、简称" prop="NickName">
          <a-input v-model="entity.NickName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="缩略图" prop="IconPath">
          <c-upload-img v-model="entity.IconPath" :maxCount="1"></c-upload-img>
        </a-form-model-item>
        <a-form-model-item label="销售价" prop="Price">
          <a-input v-model="entity.Price" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="商品分类" prop="Type">
          <a-tree-select
            v-model="entity.TypeId"
            style="width: 100%"
            :dropdown-style="{ maxHeight: '400px', overflow: 'auto' }"
            :tree-data="treeData"
            placeholder="请选择商品分类"
            tree-default-expand-all
          >
          </a-tree-select>
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
      entity: { IconPath: '' },
      rules: {},
      title: '',
      treeData: []
    }
  },
  methods: {
    init() {
      this.visible = true
      this.entity = { IconPath: '' }
      this.$nextTick(() => {
        this.$refs['form'].clearValidate()
      })
      this.$http.post('/ShoppingMall/ProductType/BasicProductType/GetDataList', {}).then(resJson => {
        if (resJson.Success) {
          this.treeData = resJson.Data
        }
      })
    },
    openForm(id, title) {
      this.init()

      if (id) {
        this.loading = true
        this.$http.post('/ShoppingMall/Product/BasicProduct/GetTheData', { id: id }).then(resJson => {
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
        this.$http.post('/ShoppingMall/Product/BasicProduct/SaveData', this.entity).then(resJson => {
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
