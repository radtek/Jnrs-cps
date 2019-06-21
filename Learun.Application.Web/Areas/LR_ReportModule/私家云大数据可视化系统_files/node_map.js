
$.getJSON('js/flights.json', function (result) {

	var myChart = echarts.init(document.getElementById('nodeChartGlobe'));
    var allData=[];
    var series = [];  
    var legendData = [];

    //把所有数据保存到allData
    for (key in result) {
        for (i in result[key]) {
            allData.push(result[key][i])
        };
    }
    result.All = allData;

    //生成排序后的新数组,并且取前11名(All占了一个位置)
    var data = [];
    for (key in result) {
        data.push({country:key,value:result[key]})
    };
    data.sort(function (a,b){
        return b.value.length - a.value.length;
    });
    data = data.slice(0,11);

    //添加点数据
    for (i in data) {
        series.push({
            name: data[i].country,
            type: 'scatter3D',
            coordinateSystem: 'globe',
            blendMode: 'lighter',
            symbolSize: 2,
            itemStyle: {
                color: 'rgb(50, 50, 150)',
                opacity: 1
            },
            silent: true,
            data: data[i].value
        });
        legendData.push({
            name: data[i].country,
            icon: 'none',
            textStyle: {
                color: '#00ffc6'
            }
        })
    };

    //把点数据转换为线
    function convertLine(data) {
        var lineData= [];
        // data.sort(function (a,b){
        //     return a[0] - b[0] && a[1] - b[1];//按经纬度排序
        // });
        for(var i = 0; i<data.length-1; i++){
            var str = [data[i],data[i+1]];//获取临近的两点
            lineData.push(str);
        };
        return lineData;
    }
    //添加线数据
    for (i in data) {
        series.push({
            type: 'lines3D',
            name: data[i].country,
            effect: {
                show: true,
                period: 5,
                trailWidth: 1,
                trailLength: 0.15,
                trailOpacity: 1,
                trailColor: 'rgb(30, 30, 60)'
            },
            lineStyle: {
                width: 1,
                color: 'rgb(50, 50, 150)',
                opacity: data[i].country=='All'?0.3:0.8
            },
            blendMode: 'lighter',
            data: convertLine(data[i].value)
        })
    }

    //获取世界地图
    $.getJSON('js/map_world.json', function (mapJson) {
        echarts.registerMap('map_world', mapJson);
        var canvas = document.createElement('canvas');
        var worldMap = echarts.init(canvas, null, {
            width: 4096, height: 2048
        });
        worldMap.setOption({      
            series: [
                {
                    type: 'map',
                    map: 'map_world',
                    top: 0, left: 0,
                    right: 0, bottom: 0,
                    selectedMode: 'single',
                    itemStyle: {
                        areaColor: 'transparent',
                        borderColor: '#00b0ff',
                        borderWidth: '1',
                    },
                    emphasis: {
                        itemStyle: {
                            areaColor: 'transparent',
                            borderColor: '#00ffc6',
                            borderWidth: '2'
                        },
                        label: {
                            show: true,
                            color: '#00ffc6',
                            fontSize: 14,
                        }
                    }
                }
            ]
        });

        //渲染全球节点
        myChart.setOption({
            color: '#00ffc6',
            legend: {
                selectedMode: 'single',
                orient: 'vertical',
                top: 'center',
                left: 15,
                padding: 0,
                itemGap: 20,
                itemWidth: 0,
                formatter: function (name) {                  
                    return "●  "+ name +"  "+result[name].length;
                },
                data: legendData,
                inactiveColor: 'rgba(255,255,255,0.8)',
                textStyle: {
                    color: '#fff',
                    textBorderColor: "rgb(22,25,48)",
                    textBorderWidth: 2
                }
            },
            globe: {
                environment: 'none',
                globeRadius: 70,
                left: 0,
                baseTexture: worldMap,
                shading: 'color',
                viewControl: {
                    autoRotate: true,
                    targetCoord: [100, 20]
                }
            },
            series: series
        });

        //左侧legend事件
        $.getJSON('js/country_coord.json', function (result) {//获取每个国家的坐标
            myChart.on('legendselectchanged', function(params) {
                var country = params.name;
                var coord = country=='All'?[100,20]:result[country];
                myChart.setOption({
                    globe:{
                        viewControl:{
                            autoRotate: country=='All',
                            targetCoord: coord,
                            distance: country=='All'?150:100
                        }
                    }
                });
                worldMap.dispatchAction({
                    type: 'mapSelect',
                    name: country
                });
            })
        })

    });    
});
