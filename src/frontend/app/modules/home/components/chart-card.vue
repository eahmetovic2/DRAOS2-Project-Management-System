<template>
  <material-card
    v-bind="$attrs"
    class="v-card--material-chart"
    v-on="$listeners"
  >
    <bar-chart
      v-if="type=='Bar'"
      slot="header"
      :height="200"
      :chartdata="data"
      :options="bar_options"/>

    <line-chart
      v-if="type=='Line'"
      slot="header"
      :height="200"
      :chartdata="data"
      :options="line_options"/>
    <slot /> 

    <slot
      slot="actions"
      name="actions"
    />
  </material-card>
</template>

<script>
import LineChart from './charts/line-chart.vue'
import BarChart from './charts/bar-chart.vue'
export default {
    name: 'ChartCard',
    components: {
      LineChart,
      BarChart,
    },
    data() {
      return { 
      }
    },
  inheritAttrs: false,

  props: {
    data: {
      type: Object,
      default: () => ({})
    },
    eventHandlers: {
      type: Array,
      default: () => ([])
    },
    line_options: {
      type: Object,
      default: () => ( {
        scales: {
          yAxes: [{
            ticks: {
                beginAtZero: true,
                fontColor: 'rgba(255, 255, 255, 0.7)',
            },
            gridLines: {
              color: 'rgba(255, 255, 255, 0.2)',
              borderDash: [1],
              borderDashOffset: 0.8,
              lineWidth: 2,
              drawBorder: false,
              zeroLineColor: 'rgba(255, 255, 255, 0.2)',
            }
          }],
          xAxes: [{
            ticks: {
                fontColor: 'rgba(255, 255, 255, 0.7)',
            },
            gridLines: {
              color: 'rgba(255, 255, 255, 0.2)',
              borderDash: [1],
              borderDashOffset: 0.8,
              lineWidth: 2,
              drawBorder: false,
              zeroLineColor: 'rgba(255, 255, 255, 0.2)',
            }
          }]
        },
        responsive:true,
        maintainAspectRatio: false,
        legend: {
          display: false,
        },
        layout: {
          padding: {
            left: 5,
            right: 15,
            top: 15,
            bottom: 5
          }
        }
      })
    },
    ratio: {
      type: String,
      default: undefined
    },
    responsiveOptions: {
      type: Array,
      default: () => ([])
    },
    type: {
      type: String,
      required: true,
      validator: v => ['Bar', 'Line', 'Pie'].includes(v)
    },
    bar_options: {
      type: Object,
      default: () => ( {
        scales: {
          yAxes: [{            
            ticks: {
                fontColor: 'rgba(255, 255, 255, 0.7)',
            },
            gridLines: {
              color: 'rgba(255, 255, 255, 0.2)',
              borderDash: [1],
              borderDashOffset: 0.8,
              lineWidth: 2,
              drawBorder: false,
              zeroLineColor: 'rgba(255, 255, 255, 0.2)',
            }
          }],
          xAxes: [{
            barThickness: 15,
            ticks: {
                fontColor: 'rgba(255, 255, 255, 0.7)',
            },
            gridLines: {
              display: false,
              color: 'rgba(255, 255, 255, 0.2)',
              borderDash: [1],
              borderDashOffset: 0.8,
              lineWidth: 2,
              drawBorder: false,
              zeroLineColor: 'rgba(255, 255, 255, 0.2)',
            }
          }]
        },
        responsive:true,
        maintainAspectRatio: false,
        legend: {
          display: false,
        },
        layout: {
          padding: {
            left: 5,
            right: 15,
            top: 15,
            bottom: 5
          }
        }
      })
    },
  }
}
</script>

<style lang="scss">
  .v-card--material-chart {
    .v-card--material__header {
      .ct-label {
        color: inherit;
        opacity: .7;
        font-size: 0.975rem;
        font-weight: 100;
      }

      .ct-grid{
        stroke: rgba(255, 255, 255, 0.2);
      }
      .ct-series-a .ct-point,
      .ct-series-a .ct-line,
      .ct-series-a .ct-bar,
      .ct-series-a .ct-slice-donut {
          stroke: rgba(255,255,255,.8);
      }
      .ct-series-a .ct-slice-pie,
      .ct-series-a .ct-area {
          fill: rgba(255,255,255,.4);
      }
    }
  }
</style>
