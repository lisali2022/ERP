<template>
  <div class="home-box">
    <el-row :gutter="20" class="top-div">
      <el-col :span="17">
        <div class="chat-hub">
          <!--          <p @click="onClickToChatHub">点击前往-最新上线<span>《聊天室》 </span>，现已支持<span>Ai助手</span>，希望能帮助大家</p>-->

          <p v-if="isIcp"
             style="font-size: 25px;
    color: blue;
    background: red;
    /* height: 80px; */
    font-weight: 900;
    text-align: center;
margin: 10px auto;">
            本站点为个人内容分享，全部资料免费开源学习，所有数据为假数据
            <br/>
            不涉及企业、团体、论坛和经营销售等内容，只做简单的成果展示
            <br/>
            富强、‌民主、文明、‌和谐、‌自由、‌平等
            <br/>
            公正、‌法治、‌爱国、‌敬业、‌诚信、友善
          </p>
          <p v-else @click="onClickToWeChat">
            点击关注-最新上线<span>《意.Net官方微信公众号》 </span>，分享有<span>深度</span>的.Net知识，希望能帮助大家</p>


        </div>
        <div class="scrollbar">
          <ScrollbarInfo/>
        </div>

        <el-row class="left-div">


          <el-col :span="8" v-for="i in plateList" :key="i.id" class="plate" :style="{
      'padding-left': i % 3 == 1 ? 0 : 0.2 + 'rem',
      'padding-right': i % 3 == 0 ? 0 : 0.2 + 'rem',
    }">
            <img v-if="isIcp" src="@/assets/login.png" style="height: 80px;width: 100%" alt=""/>
            <PlateCard v-else :name="i.name" :introduction="i.introduction" :id="i.id"
                       :isPublish="i.isDisableCreateDiscuss"/>
          </el-col>

          <div ref="scrollableDiv" class="scrollable-div" v-infinite-scroll="loadDiscuss" style="height: 2500px;width: 100%; overflow-y: auto;" infinite-scroll-distance="10">

            <el-col v-if="isDiscussFinished" :span="24" v-for="i in discussList" :key="i.id">
              <img v-if="isIcp" src="@/assets/login.png" style="height: 150px;width: 100%" alt=""/>
              <DisscussCard v-else :discuss="i" badge="置顶"/>
            </el-col>
            <Skeleton v-else :isBorder="true"/>
       

            <el-col :span="24" v-for="i in allDiscussList" :key="i.id">
              <img v-if="isIcp" src="@/assets/login.png" style="height: 150px;width: 100%" alt=""/>
              <DisscussCard v-else :discuss="i"/>
            </el-col>

            <Skeleton v-if="!isAllDiscussFinished"  :isBorder="true"/>
          </div>

 
        </el-row>
      </el-col>
      <el-col :span="7">
        <el-row class="right-div">
          <el-col :span="24">
            <el-carousel trigger="click" height="150px">
              <el-carousel-item v-for="item in bannerList" :key="item.id">
                <div class="carousel-font" :style="{ color: item.color }">
                  {{ item.name }}
                </div>
                <el-image style="width: 100%; height: 100%" :src="item.logo" fit="cover"/>
              </el-carousel-item>
            </el-carousel>
          </el-col>
          <div class="analyse">
            <div class="item">
              <div class="text">在线人数</div>
              <div class="content">
                <div class="name"></div>
                <div class="content-box top">
                  <div class="count">{{ onlineNumber }}</div>
                </div>
              </div>
            </div>
            <div class="item">
              <div class="text">注册人数</div>
              <div class="content">
                <div class="content-box top">
                  <div class="count">{{ userAnalyseInfo.registerNumber }}</div>
                </div>
              </div>
            </div>
            <div class="item">
              <div class="text">昨日新增</div>
              <div class="content">
                <div class="content-box">
                  <div class="count">
                    {{ userAnalyseInfo.yesterdayNewUser }}
                  </div>
                </div>
              </div>
            </div>
          </div>
          <!-- 签到 -->
          <el-col v-if="!isIcp" :span="24">
            <InfoCard header="活动">
              <template #content>
                <div class="top">你好，很高兴今天又遇到你呀~</div>
                <el-row class="active">

                  <el-col v-for="item in activeList" :span="6" @click="handleToRouter(item.path)">

                    <el-icon color="#70aafb" size="30px">
                      <component :is="item.icon"></component>
                    </el-icon>
                    <span> {{ item.name }}</span>
                  </el-col>
                </el-row>
              </template>
            </InfoCard>
          </el-col>

          <el-col :span="24">
            <InfoCard header="访问统计" class="VisitsLineChart" text="全站历史统计" @onClickText="onClickAccessLog">
              <template #content>
                <p class="switch-span" @click="onClickWeekSwitch">切换</p>
                <VisitsLineChart :option="statisOptions" class="statisChart"/>

              </template>
            </InfoCard>

            <el-dialog v-model="accessLogDialogVisible" title="全站历史统计" width="1200px" center>
              <el-tabs v-model="accessLogTab">
                <el-tab-pane label="访问统计（近3月）" name="AccessLogChart"
                             style="display: flex;justify-content: center;">
                  <AccessLogChart :option="accessLogOptins" style="height: 600px;width: 1200px;"/>
                </el-tab-pane>
                <el-tab-pane label="注册统计（近3月）" name="RegisterChart"
                             style="display: flex;justify-content: center;">
                  <AccessLogChart :option="registerLogOptins" style="height: 600px;width: 1200px;"/>
                </el-tab-pane>

              </el-tabs>


            </el-dialog>
          </el-col>

          <el-col :span="24">
            <InfoCard header="简介" text="">
              <template #content>
                <div class="introduce">
                  没有什么能够阻挡，人类对代码<span style="color: #1890ff">优雅</span>的追求
                </div>
              </template>
            </InfoCard>
          </el-col>

          <el-col v-if="!isIcp" :span="24">
            <template v-if="isPointFinished">
              <InfoCard :items="pointList" header="财富排行榜" text="查看我的位置" height="400"
                        @onClickText="onClickMoneyTop">
                <template #item="temp">
                  <PointsRanking :pointsData="temp"/>
                </template>
              </InfoCard>
            </template>
            <template v-else>
              <InfoCard header="本月排行" text="更多">
                <template #content>
                  <Skeleton/>
                </template>
              </InfoCard>
            </template>
          </el-col>

          <el-col v-if="!isIcp" :span="24">
            <template v-if="isFriendFinished">
              <InfoCard :items="friendList" header="推荐好友" text="更多" height="400">
                <template #item="temp">
                  <RecommendFriend :friendData="temp"/>
                </template>
              </InfoCard>
            </template>
            <template v-else>
              <InfoCard header="推荐好友" text="更多">
                <template #content>
                  <Skeleton/>
                </template>
              </InfoCard>
            </template>
          </el-col>
          <el-col v-if="!isIcp" :span="24">
            <template v-if="isThemeFinished">
              <InfoCard :items="themeList" header="推荐主题" text="更多" height="400">
                <template #item="temp">
                  <ThemeData :themeData="temp"/>
                </template>
              </InfoCard>
            </template>
            <template v-else>
              <InfoCard header="推荐主题" text="更多">
                <template #content>
                  <Skeleton/>
                </template>
              </InfoCard>
            </template>
          </el-col>

          <el-col :span="24" style="background: transparent">
            <BottomInfo/>
          </el-col>
        </el-row>
      </el-col>
    </el-row>


    <el-dialog
        v-model="wechatDialogVisible"
        title="意社区官方微信公众号"
        width="800"
    >
      <div style="display: flex;justify-content: center;">
        <img style="width: 585px; height: 186px" src="@/assets/wechat/share.png" alt=""/>
      </div>

      <template #footer>
        <div class="dialog-footer">
          <el-button type="primary" @click="wechatDialogVisible = false">
            已关注
          </el-button>
        </div>
      </template>
    </el-dialog>
    <el-backtop :right="100" :bottom="100" @click="clickBacktop" visibility-height="0"/>
  </div>
</template>

<script setup>
import {onMounted, ref, reactive, computed, nextTick, watch} from "vue";
import {useRouter} from "vue-router";
import DisscussCard from "@/components/DisscussCard.vue";
import InfoCard from "@/components/InfoCard.vue";
import PlateCard from "@/components/PlateCard.vue";
import ScrollbarInfo from "@/components/ScrollbarInfo.vue";
import BottomInfo from "@/components/BottomInfo.vue";
import VisitsLineChart from "./components/VisitsLineChart/index.vue";
import AccessLogChart from "./components/AccessLogChart/Index.vue"
import {access, getAccessList} from "@/apis/accessApi.js";
import {getList} from "@/apis/plateApi.js";
import {getList as bannerGetList} from "@/apis/bannerApi.js";
import {getHomeDiscuss} from "@/apis/discussApi.js";
import {getWeek} from "@/apis/accessApi.js";
import {
  getRecommendedTopic,
  getRecommendedFriend,
  getRankingPoints,
  getUserAnalyse,
  getRegisterAnalyse
} from "@/apis/analyseApi.js";
import {getList as getAllDiscussList} from "@/apis/discussApi.js";
import PointsRanking from "./components/PointsRanking/index.vue";
import RecommendFriend from "./components/RecommendFriend/index.vue";
import ThemeData from "./components/RecommendTheme/index.vue";
import Skeleton from "@/components/Skeleton/index.vue";
import useSocketStore from "@/stores/socket";

const accessLogDialogVisible = ref(false)
const router = useRouter();

const accessAllList = ref([]);
const registerAllList = ref([]);

const plateList = ref([]);
const discussList = ref([]);
const isDiscussFinished = ref(false);
const bannerList = ref([]);
const weekList = ref([]);
const pointList = ref([]);
const isPointFinished = ref(false);
const friendList = ref([]);
const isFriendFinished = ref(false);
const themeList = ref([]);
const isThemeFinished = ref(false);
const allDiscussList = ref([]);
const isAllDiscussFinished = ref(false);
const userAnalyseInfo = ref({});
const onlineNumber = ref(0);
const accessLogTab = ref()

const currentDiscussPageIndex = ref(1);
const activeList = [
  {name: "签到", path: "/activity/sign", icon: "Present"},
  {name: "等级", path: "/activity/level", icon: "Ticket"},
  {name: "大转盘", path: "/activity/lucky", icon: "Sunny"},
  {name: "银行", path: "/activity/bank", icon: "CreditCard"},

  {name: "任务", path: "/activity/assignment", icon: "Memo"},
  {name: "排行榜", path: "/money", icon: "Money"},
  {name: "开始", path: "/start", icon: "Position"},
  {name: "聊天室", path: "/chat", icon: "ChatRound"},
];
const isIcp = import.meta.env.VITE_APP_ICP === "true";

const weekQuery = reactive({accessLogType: "Request"});

const init = async () => {

  //分阶段优化
  await Promise.all([
    (async () => {
      const {data: allDiscussData, config: allDiscussConfig} =
          await getAllDiscussList({Type: 0, skipCount: currentDiscussPageIndex.value, maxResultCount: 10});
      isAllDiscussFinished.value = allDiscussConfig.isFinish;
      allDiscussList.value = allDiscussData.items;
    })(),
    (async () => {
      const {data: plateData} = await getList();
      plateList.value = plateData.items;
    })(),
    (async () => {
      const {data: discussData, config: discussConfig} = await getHomeDiscuss();
      discussList.value = discussData;
      isDiscussFinished.value = discussConfig.isFinish;
    })(),
    (async () => {
      const {data: bannerData} = await bannerGetList();
      bannerList.value = bannerData.items;
    })(),
    (async () => {
      const {data: weekData} = await getWeek(weekQuery);
      weekList.value = weekData;
    })(),
    (async () => {
      const {data: pointData, config: pointConfig} = await getRankingPoints();
      pointList.value = pointData.items;
      isPointFinished.value = pointConfig.isFinish;
    })(),
    (async () => {
      const {data: userAnalyseInfoData} = await getUserAnalyse();
      onlineNumber.value = userAnalyseInfoData.onlineNumber;
      userAnalyseInfo.value = userAnalyseInfoData;
    })(),
  ]);


  //不重要的请求滞后
  const {data: friendData, config: friendConfig} = await getRecommendedFriend();
  friendList.value = friendData;
  isFriendFinished.value = friendConfig.isFinish;
  const {data: themeData, config: themeConfig} = await getRecommendedTopic();
  themeList.value = themeData;
  isThemeFinished.value = themeConfig.isFinish;
  await access();
}
//初始化
onMounted(async () => {
  await init();
});

const weekXAxis = ["周一", "周二", "周三", "周四", "周五", "周六", "周日"];
// 访问统计
const statisOptions = computed(() => {
  return {
    xAxis: {
      data: weekList.value.map((item, index) => {
        return weekXAxis.filter((v, vIndex) => {
          return vIndex === index;
        })[0];
      }),
    },
    series: {
      data: weekList.value.map((item) => item.number),
    },
  };
});
//历史全部访问统计
const accessLogOptins = computed(() => {
  return {
    xAxis: {
      data: accessAllList.value?.map((item, index) => {
        return item.creationTime.slice(0, 10);

      })
    },
    series: [
      {
        data: accessAllList.value?.map((item, index) => {
          return item.number;
        })
      }
    ]
  }
});

//历史注册人员全部访问统计
const registerLogOptins = computed(() => {
  return {
    xAxis: {
      data: registerAllList.value?.map((item, index) => {
        return item.time.slice(0, 10);

      })
    },
    series: [
      {
        data: registerAllList.value?.map((item, index) => {
          return item.number;
        })
      }
    ]
  }
});

const onClickMoneyTop = () => {

  router.push("/money");
};

const onClickToChatHub = () => {
  router.push("/chat");
};

const handleToRouter = (path) => {
  router.push(path);
};

// 推送的实时人数获取
const currentOnlineNum = computed(() => useSocketStore().getOnlineNum());
watch(
    () => currentOnlineNum.value,
    (val) => {
      onlineNumber.value = val;
    },
    {deep: true}
);
watch(
    () => accessLogTab.value,
    async (value) => {
      switch (value) {
        case "AccessLogChart":
          const {data} = await getAccessList(weekQuery);
          accessAllList.value = data;

          break;
        case "RegisterChart":
          const {data: registerUserListData} = await getRegisterAnalyse();
          registerAllList.value = registerUserListData;

          break;
      }
    }
)
const onClickAccessLog = async () => {
  accessLogDialogVisible.value = true;
  accessLogTab.value = "AccessLogChart";

}

let loadingDiscuss = false;
//加载滚动文章
const loadDiscuss = async () => {
  
  if (loadingDiscuss === false) {
    loadingDiscuss = true;


    currentDiscussPageIndex.value += 1;

    isAllDiscussFinished.value = false;
    const {data: allDiscussData, config: allDiscussConfig} =
        await getAllDiscussList({Type: 0, skipCount: currentDiscussPageIndex.value, maxResultCount: 10});
    isAllDiscussFinished.value = allDiscussConfig.isFinish;

    //在列表后新增
    allDiscussList.value.push(...allDiscussData.items);


    loadingDiscuss = false;
  }


}
const wechatDialogVisible = ref(false)
//切换统计开关
const onClickWeekSwitch = async () => {
  if (weekQuery.accessLogType === "HomeClick") {
    weekQuery.accessLogType = "Request";
  } else if (weekQuery.accessLogType === "Request") {
    weekQuery.accessLogType = "HomeClick";
  }

  const {data: weekData} = await getWeek(weekQuery);
  weekList.value = weekData;
}
const scrollableDiv = ref(null);
//回到顶部
const clickBacktop=()=>{
  if (scrollableDiv.value) {
    scrollableDiv.value.scrollTop = 0; // 设置滚动条到顶部
  }
}

//打开微信公众号弹窗
const onClickToWeChat = () => {
  wechatDialogVisible.value = true;
};
</script>
<style scoped lang="scss">
.home-box {
  width: 1300px;
  height: 100%;

  .introduce {
    color: rgba(0, 0, 0, 0.45);
    font-size: small;
  }

  .plate {
    background: transparent !important;
  }

  .left-div .el-col {
    background-color: #ffffff;

    margin-bottom: 1rem;
  }

  .right-div .el-col {
    background-color: #ffffff;
    margin-bottom: 1rem;
  }

  .carousel-font {
    position: absolute;
    z-index: 1;
    top: 10%;
    left: 10%;
  }

  .top-div {
    padding-top: 0.5rem;
  }

  .scrollbar {
    display: block;
    margin-bottom: 0.5rem;
  }

  .analyse {
    display: flex;
    justify-content: space-between;
    align-items: center;
    width: 100%;
    height: 100px;
    margin-bottom: 10px;

    .item {
      width: 30%;
      height: 100%;
      position: relative;
      background: url("@/assets/box/online_bg.svg") no-repeat;
      background-color: #fff;
      background-position: 0 30px;
      background-size: 150% 100%;
      border: 1px solid #409eff;
      border-radius: 5px;
      color: #409eff;

      .content {
        width: 100%;
        height: 100%;
        display: flex;
        justify-content: center;

        &-box {
          width: 100%;
          display: flex;
          align-items: center;
          justify-content: flex-start;
          justify-content: center;
          margin-bottom: 10px;

          .name {
            font-size: 14px;
          }

          .count {
            font-size: 20px;
            font-weight: bold;
          }
        }
      }

      .text {
        width: 60px;
        position: absolute;
        top: -10px;
        left: 50%;
        transform: translateX(-50%);
        font-size: 12px;
        text-align: center;
        border: 1px solid #d9ecff;
        border-radius: 5px;
        color: #409eff;
        background-color: #ecf5ff;
      }
    }
  }

  .top {
    text-align: center;
    margin-bottom: 20px;
  }

  .active {
    display: flex;
    justify-content: space-between;
    align-items: center;
    color: #8a919f;


    .el-col {
      flex-direction: column;
      align-items: center;
      display: flex;
      cursor: pointer;
      padding: 10px 0px;
    }

    .el-col:hover {
      background-color: #cce1ff;
      /* 悬浮时背景色变化 */
      color: #70aafb;
      /* 悬浮时文字颜色变化 */
    }

    &-btn {
      cursor: pointer;
      display: flex;
      align-items: center;
      justify-content: center;
      width: 74px;
      height: 36px;
      border-radius: 4px;
      border: 1px solid rgba(30, 128, 255, 0.3);
      background-color: rgba(30, 128, 255, 0.1);
      color: #1e80ff;
    }
  }

  .VisitsLineChart > > > .el-card__body {
    padding: 0.5rem;
  }

  .VisitsLineChart p {
    display: flex;
    justify-content: flex-end;
    color: #409eff;
    cursor: pointer;
    margin-top: 8px;
  }

  .statisChart {
    width: 100%;
    height: 300px;
  }

  .accessLogChart {
    width: 1100px;
    height: 500px;
  }
}


//走马灯，聊天室链接
.chat-hub {
  background-color: #E6A23C;
  color: #ffffff;
  margin-bottom: 10px;
  width: 100%;
  overflow: hidden;
  white-space: nowrap;
  box-sizing: border-box;

  span {
    color: red;
  }

  display: flex;
  align-content: center;
  flex-wrap: wrap;
  min-height: 30px;

  p {
    margin: 0 auto;
    cursor: pointer;
  }
}

@keyframes marquee {
  0% {
    transform: translateX(0);
  }

  100% {
    transform: translateX(-100%);
  }
}


/* 设置滚动条的样式 */
.scrollable-div::-webkit-scrollbar {
  width: 3px; /* 设置滚动条的宽度 */
}

.scrollable-div::-webkit-scrollbar-track {
  background: #f1f1f1; /* 滚动条轨道背景 */
}

.scrollable-div::-webkit-scrollbar-thumb {
  background: #cccccc; /* 滚动条的颜色 */
  border-radius: 10px; /* 设置圆角 */
}

.scrollable-div::-webkit-scrollbar-thumb:hover {
  background: #555; /* 滚动条 hover 时的颜色 */
}
</style>
