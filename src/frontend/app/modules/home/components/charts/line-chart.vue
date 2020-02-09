<script>
import { Line } from 'vue-chartjs'

export default {
  extends: Line,
  props: {
    chartdata: {
      type: Object,
      default: null
    },
    options: {
      type: Object,
      default: null
    }
  },
  mounted () {
    this.renderChart(this.chartdata, this.options)
  },
  watch: {
    chartdata(newValue, oldValue) {
      console.log("IZMJENA", newValue)
      if (newValue !== oldValue) {
        this.renderChart(newValue, this.options)
      }
    }
  }
}
</script>
