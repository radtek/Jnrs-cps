$(document).ready(function(){

	$('#toggle').click(function(){
		$('.mainLeft, .mainRight, .mainBottom').fadeToggle(500);
		$(this).toggleClass('active');
	})
    setInterval(function () {
        var now = new Date();
        document.getElementById('time1').innerHTML = "设备概况 截止：" + now.getFullYear() + "-" + (now.getMonth() + 1) + "-" + now.getDate();
        document.getElementById('time').innerHTML = now.getFullYear() + "-" + (now.getMonth() + 1) + "-" + now.getDate() + " " + now.getHours() + ":" + now.getMinutes() + ":" + now.getSeconds();
    }, 1000);
// 设备饼状图 -------------------------------------------------------
	var deviceChartPie;
	// $.get('url',function(result){
	function renderDeviceChartPie() {

	    deviceChartPie = echarts.init(document.getElementById('deviceChartPie'));	

		let DataSeries = [
			{value:18363, name:'在线设备'},
		    {value:5358, name:'离线设备'}
	    ];
		let DataLegend = ['在线设备','离线设备']; 	
		let dataOption = {
			title: { text:'' },
			legend: { data : DataLegend, },
		    series: [{ data: DataSeries, }]
		}

		deviceChartPie.setOption(pieOption);
		deviceChartPie.setOption(dataOption);
	}
	// });


// 用户饼状图 -------------------------------------------------------
	var userChartPie;
	// $.get('url',function(result){
	function renderUserChartPie() {

	    userChartPie = echarts.init(document.getElementById('userChartPie'));	

		let DataSeries = [
			{value:5358, name:'日活跃用户'},
		    {value:18568, name:'非日活跃用户'}
	    ];
		let DataLegend = ['日活跃用户','非日活跃用户']; 		
		let dataOption = {
			title: { text:'' },
			legend: { data : DataLegend, },
		    series: [{ data: DataSeries, }]
		}

		userChartPie.setOption(pieOption);
		userChartPie.setOption(dataOption);
	}
	// });
    function InitDate(yesday) {

        return  yesday.getFullYear() + "-" + (yesday.getMonth() > 9 ? (yesday.getMonth() + 1) : "0" + (yesday.getMonth() + 1)) + "-" + (yesday.getDate() > 9 ? (yesday.getDate()) : "0" + (yesday.getDate())); 
    }

// 设备曲线图 -------------------------------------------------------
	var deviceChartCurve;
	// $.get('url',function(result){
	function renderDeviceChartCurve() {

		deviceChartCurve = echarts.init(document.getElementById('deviceChartCurve'));
        var curDate = new Date();
        curDates = InitDate(curDate);
        var preDate1 = new Date(curDate.getTime() - 24 * 60 * 60 * 1000); //前1天
        preDate1 = InitDate(preDate1);
        var preDate2 = new Date(curDate.getTime() - 24 * 60 * 60 * 1000 * 2); //前2天
        preDate2 = InitDate(preDate2);
        var preDate3 = new Date(curDate.getTime() - 24 * 60 * 60 * 1000 * 3); //前3天
        preDate3 = InitDate(preDate3);
        var preDate4 = new Date(curDate.getTime() - 24 * 60 * 60 * 1000 * 4); //前4天
        preDate4 = InitDate(preDate4);
        var preDate5 = new Date(curDate.getTime() - 24 * 60 * 60 * 1000 * 5); //前5天
        preDate5 = InitDate(preDate5);
        var preDate6 = new Date(curDate.getTime() - 24 * 60 * 60 * 1000 * 6); //前6天
        preDate6 = InitDate(preDate6);
        let DataX = [preDate5, preDate4, preDate3, preDate2, preDate1, curDates];
		//let DataX = ['2018/03','2018/04','2018/05','2018/06','2018/07','2018/08'];
        let DataY = ['25', '21.5', '23.8', '15.4', '17.9','23.38'];
		let dataOption = {
			title: { text:'开动率%' },
            tooltip: { formatter: "开动率%：{c}<br/>时间：{b}" },
			xAxis: { data : DataX },
		    series: [{ data: DataY }]
		}

		deviceChartCurve.setOption(curveOption);			
		deviceChartCurve.setOption(dataOption);
	}
	// })


// 用户曲线图 -------------------------------------------------------
	var deviceChartCurve;
	// $.get('url',function(result){
	function renderUserChartCurve() {

		userChartCurve = echarts.init(document.getElementById('userChartCurve'));
        var curDate = new Date();
        curDates = InitDate(curDate);
        var preDate1 = new Date(curDate.getTime() - 24 * 60 * 60 * 1000); //前1天
        preDate1 = InitDate(preDate1);
        var preDate2 = new Date(curDate.getTime() - 24 * 60 * 60 * 1000 * 2); //前2天
        preDate2 = InitDate(preDate2);
        var preDate3 = new Date(curDate.getTime() - 24 * 60 * 60 * 1000 * 3); //前3天
        preDate3 = InitDate(preDate3);
        var preDate4 = new Date(curDate.getTime() - 24 * 60 * 60 * 1000 * 4); //前4天
        preDate4 = InitDate(preDate4);
        var preDate5 = new Date(curDate.getTime() - 24 * 60 * 60 * 1000 * 5); //前5天
        preDate5 = InitDate(preDate5);
        var preDate6 = new Date(curDate.getTime() - 24 * 60 * 60 * 1000 * 6); //前6天
        preDate6 = InitDate(preDate6);
        let DataX = [preDate5, preDate4, preDate3, preDate2, preDate1, curDates];
		//let DataX = ['2018/03','2018/04','2018/05','2018/06','2018/07','2018/08'];
		let DataY = ['76','68','84','93','36','69'];
		let dataOption = {
			title: { text:'月产量分析' },
			tooltip: { formatter: "数量：{c}<br/>时间：{b}" },
			xAxis: { data : DataX },
		    series: [{ data: DataY }]
		}

		userChartCurve.setOption(curveOption);			
		userChartCurve.setOption(dataOption);
	}
	// })


// 城市排行柱状图 -------------------------------------------------------
	var cityChartStick;
	// $.get('url',function(result){
	function rendercityChartStick() {

		cityChartStick = echarts.init(document.getElementById('cityChartStick'));

		let DataX = ['周一','周二','周三','周四','周五','周六','周天'];
		let DataY = [78,89,101,125,148,170,202];
		let dataOption = {
			title: { text:'' },
			tooltip: { formatter: "数量：{c}" },
			xAxis: { data : DataX },
			yAxis: { data : DataY},
		    series: [{ data: DataY }]
		}

		cityChartStick.setOption(stickOption);			
		cityChartStick.setOption(dataOption);
	}
	// })


// 行情K线图 -------------------------------------------------------
	var btrChartKline;
	// $.get('url',function(result){
	function renderbtrChartKline() {

		btrChartKline = echarts.init(document.getElementById('btrChartKline'));

        let DataX = ['一', '二', '三', '四', '五', '六', '七','八', '九', '十', '十一', '十二', '十三', '十四','十五', '十六', '十七', '十八', '十九', '二十', '二十一','二十二', '二十三', '二十四', '二十五', '二十六', '二十七', '二十八', '二十九', '三十'];
        let DataY = [28, 39, 20, 22, 19, 20, 25, 28, 29, 30, 12, 19, 20, 25, 18, 29, 20, 42, 39, 19, 19, 20, 30, 40, 20, 30, 20, 30, 33, 20];
        var now = new Date();
        var index = now.getDate();
        DataY.splice(index, 30 - index);
        let dataOption = {
            title: { text: '本月OEE%' },
            tooltip: { formatter: "OEE：{c}<br/>时间：{b}" },
            xAxis: { data: DataX },
            series: [{ data: DataY }]
        }

		btrChartKline.setOption(klineOption);			
		btrChartKline.setOption(dataOption);
	}
	// })


// K线图样式 -------------------------------------------------------
	var klineOption = {	
        color: '#00ffc7',
        title: {
            text: '',
            textStyle: {
                color: '#00b0ff',
                fontSize: 12,
                fontWeight: 'normal',
                // textBorderColor: '#ffffff',
                // textBorderWidth: 1,
            },
            padding: 0,
            left: 20,
            top: 10,
        },
        tooltip: {
            trigger: 'axis',
            backgroundColor: 'rgba(0,0,0,0.5)',
            borderColor: '#00ffc7',
            borderWidth: 1,
            textStyle: {
                color: '#00ffc7',
                fontSize: 12,
            },
            formatter: "",
        },
        grid: {
            show: true,
            borderColor: 'transparent',
            backgroundColor: 'transparent',
            left: '0',
            right: '0',
            top: '0',
            bottom: '0',
        },
        xAxis: {
            data: [],
            boundaryGap: false,
            axisLine: {
                lineStyle: { color: '#00b0ff' },
            },
            axisLabel: {
                textStyle: { fontSize: 10, color: '#fff' },
                inside: true,
                showMinLabel: false,
                showMaxLabel: false,
            },
            axisTick: {
                inside: true,
            },
            splitLine: {
                show: true,
                lineStyle: {
                    color: 'rgba(255,255,255,0.1)',
                    width: 1,
                },
            }
        },
        yAxis: {
            position: 'right',
            type: 'value',
            max: 'dataMax',
            axisLine: {
                lineStyle: { color: '#00b0ff' },
            },
            axisLabel: {
                textStyle: { fontSize: 10 },
                showMinLabel: false,
                showMaxLabel: false,
                inside: true,
            },
            axisTick: {
                inside: true,
            },
            splitLine: {
                lineStyle: {
                    color: 'rgba(255,255,255,0.1)',
                    width: 1,
                },
            }
        },
        series: [{
            name: '',
            type: 'line',
            lineStyle: {
                normal: { width: '2', }
            },
            symbolSize: 6,
            areaStyle: {
                normal: {
                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                        { offset: 0, color: 'rgba(0, 255, 198, 0.8)' },
                        { offset: 1, color: 'rgba(0, 255, 198, 0)' }
                    ])
                }
            },
            animation: true,
            data: [],
        }]
	}

	renderbtrChartKline()


// 饼状图样式 -------------------------------------------------------
	var pieOption = {

		color: [
			new echarts.graphic.LinearGradient(1, 0, 0, 0.5, [
        		{ offset: 0, color: '#00b0ff'},
        		{ offset: 1, color: '#00ffc6'}
    		]),
    		new echarts.graphic.LinearGradient(0, 0, 1, 1, [
        		{ offset: 0, color: 'rgba(0,0,0,0)'},
        		{ offset: 1, color: 'rgba(0,0,0,0)'}
    		])
		],

		title: {
			text: '',
			textStyle: {
				color: '#00ffc6',
				fontSize: 14,
				fontWeight: 'normal',
			},
			padding: 0,
			left: 10,
			top: 25,
		},

		tooltip: {
			show: false,
			trigger: 'item',
			backgroundColor: '#fff',
			borderColor: '#928aff',
			borderWidth: 1,
			textStyle: {
				color: '#928aff',
				fontSize: 10,
			},
			formatter: "b：{b} <br/> c：{c}",			
		},

		legend: {
	        data:[],
	        show: false,
	    },

		series : [
		    {
		        name:'',
		        type:'pie',
		        radius : ['35%', '50%'],
		        center: ['50%', '50%'],
		        data: [],
		        label: {
		            normal: {
		            	show: false,
			        	position: 'inside',
		                textStyle: {
		                    color: '#fff',
		                },
		                formatter: '{d}%'
		            },
		        },
		        labelLine: {
		            normal: {
		                lineStyle: {
		                    color: '#8992a6',
		                    type: 'dashed',
		                },
		                show: false,
		            }
		        },

		        animationType: 'expansion',
		        animationDuration: 4000,
		        animationDelay: function (idx) {
		            return Math.random() * 200;
		        }
		    }
		]
	};

	renderDeviceChartPie();
	renderUserChartPie();


// 曲线图样式 -------------------------------------------------------    
	var curveOption = {
		color: '#00ffc7',
		title: {
			text: '',
			textStyle: {
				color: '#00b0ff',
				fontSize: 12,
				fontWeight: 'normal',
	            // textBorderColor: '#ffffff',
	            // textBorderWidth: 1,
			},
			padding: 0,
			left: 20,
			top: 10,
		},
		tooltip: {
			trigger: 'axis',
			backgroundColor: 'rgba(0,0,0,0.5)',
			borderColor: '#00ffc7',
			borderWidth: 1,
			textStyle: {
				color: '#00ffc7',
				fontSize: 12,
			},
			formatter: "",
		},
	    grid: {
	        show: true,
	        borderColor: 'transparent',
	        backgroundColor: 'transparent',
	        left: '0',
	        right: '0',
	        top: '0',
	        bottom: '0',
	    },
	    xAxis: {
	        data: [],
	        boundaryGap: false,
	        axisLine: {
	            lineStyle: { color: '#00b0ff'},
	        },
	        axisLabel:{  
	            textStyle:{fontSize:10},
	        	inside: true,
		        showMinLabel: false,
		        showMaxLabel: false,
	        },
	        axisTick: {
	        	inside: true,
	        },
	        splitLine: {
	            show: true,
	            lineStyle: { 
	            	color: 'rgba(255,255,255,0.1)',
	            	width: 1,
	            },
	        }
	    },
	    yAxis: {
	        position: 'right',
	        type: 'value',
	        max: 'dataMax',
	        axisLine: {
	            lineStyle: { color: '#00b0ff'},
	        },
	        axisLabel:{  
	            textStyle:{fontSize:10},
		        showMinLabel: false,
		        showMaxLabel: false,
	            inside: true,
	        },
	        axisTick: {
	        	inside: true,
	        },
	        splitLine: {	            
	            lineStyle: { 
	            	color: 'rgba(255,255,255,0.1)',
	            	width: 1,
	            },
	        }
	    },
	    series: [{
	        name: '',
	        type: 'line',
	        lineStyle :{
	            normal: { width: '2',}
	        },
	        symbolSize: 6,
	        areaStyle: {
	            normal: {
	            	color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
	            		{ offset: 0, color: 'rgba(0, 255, 198, 0.8)'},
	            		{ offset: 1, color: 'rgba(0, 255, 198, 0)'}
            		])
	            }
	        },
	        animation: true,
	        data: [],
	    }]
	};

	renderDeviceChartCurve();
	renderUserChartCurve();

//柱状图样式 -------------------------------------------------------
	var stickOption = {
		color: '#928aff',
		title: {
			text: '',
			textStyle: {
				color: '#00ffc6',
				fontSize: 12,
				fontWeight: 'normal',
	            // textBorderColor: '#ffffff',
	            // textBorderWidth: 1,
			},
			padding: 0,
			left: 10,
			top: 0,
		},
		tooltip: {
			trigger: 'axis',
			backgroundColor: 'rgba(0,0,0,0.5)',
			borderColor: '#00ffc6',
			borderWidth: 1,
			textStyle: {
				color: '#00ffc6',
				fontSize: 12,
			},
			formatter: "",			
		},
	    grid: {
	        show: true,
	        borderColor: 'transparent',
	        backgroundColor: 'transparent',
	        left: '50',
	        right: '10',
	        top: '0',
	        bottom: '0',
	    },
	    label: {
	    	show: true,
	    	position: 'top',
	    	distance: 10,
	    	formatter: '{b}',
	    	fontSize: 12,
	    	color: '#00b0ff',
	    	textBorderWidth: '0',
	    },
	    xAxis: {
	        data: [],
	        boundaryGap: true,
	        axisLine: {
	            lineStyle: { color: '#00b0ff'},
	        },
	        axisLabel:{  
	        	show: false,
	            textStyle:{fontSize:10},
	        },
	        axisTick: {
	        	inside: true,
	        },
	        splitLine: {
	            show: true,
	            lineStyle: { 
	            	color: 'rgba(255,255,255,0.1)',
	            	width: 1,
	            },
	        }
	    },
	    yAxis: {
	        position: 'left',
	        type: 'value',
	        max: 'dataMax',
	        axisLine: {
	            lineStyle: { color: '#00b0ff'},
	        },
	        axisLabel:{  
	            textStyle:{fontSize:10},
		        showMinLabel: false,
		        showMaxLabel: false,
	            // inside: true,
	        },
	        axisTick: {
	        	inside: true,
	        },
	        splitLine: {	            
	            lineStyle: { 
	            	color: 'rgba(255,255,255,0.1)',
	            	width: 1,
	            },
	        }
	    },
	    series: [{
	        name: '',
	        type: 'bar',
	        barWidth: '60%',
	        itemStyle: {
	        	normal: {
	            	color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
	            		{ offset: 0, color: '#00ffc6'},
	            		{ offset: 1, color: 'rgba(0,176,255,0)'}
            		]),
            		borderColor: '#00b0ff',
            		borderWidth: 1,
	            },
	        },
	        lineStyle :{
	            normal: { width: '1',}
	        },
	        animation: true,
	        data: [],
	    }]
	};

	rendercityChartStick();

})