<template>
  <a-card :bordered="false">
    <a-tabs default-active-key="1">
      <a-tab-pane key="1" tab="转入">
        <data-list type="1"></data-list>
      </a-tab-pane>
      <a-tab-pane key="2" tab="转出" force-render>
        <data-list type="2"></data-list>
      </a-tab-pane>
    </a-tabs>
  </a-card>
</template>

<script>
import DataList from './DataList.vue'

export default {
  components: {
    DataList
  },
  mounted() {
    
  },
  data() {
    return {
      data: [],
    }
  },
  methods: {

  }
}
</script>