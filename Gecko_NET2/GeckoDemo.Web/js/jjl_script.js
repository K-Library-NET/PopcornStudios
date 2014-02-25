// JavaScript Document
$(function(){
		//轮播
		var index=0;
		var liw=$('#mm_home .lunbo ul li').width();
		var lilen=$('#mm_home .lunbo ul li').length;
		$('#mm_home .lunbo ul').width(liw*lilen);
		
		
		$('#mm_home .lunbo ul li').hover(function(){
			clearInterval(S);
		},function(){
			S=setInterval(function(){
			$('#mm_home .lunbo ul').animate({'left':-index*liw},300);
			$('#mm_home .lunbo div span').eq(index).addClass('on').siblings().removeClass('on'); 
			index++;
			if(index==lilen) index=0;},3000);
		}).eq(0).trigger('mouseout');
		
		$('#mm_home .lunbo div span').mouseover(function(){
			var k=$('#mm_home .lunbo div span').index(this);
			$('#mm_home .lunbo ul').stop(false,false).animate({'left':-k*liw},300);
			$('#mm_home .lunbo div span').eq(k).addClass('on').siblings().removeClass('on'); 
		});
		
		//音乐试听，下载
		$('#mm_home .music_listener span').hide();
		$('#mm_home .music_load span').hide();
		$('#mm_home .music_listener').hover(function(){
			$(this).find('a').addClass('on').parent().find('span').show();
		},function(){
			$(this).find('a').removeClass('on').parent().find('span').hide();
		});
		$('#mm_home .music_load').hover(function(){
			$(this).find('a').addClass('on').parent().find('span').show();
		},function(){
			$(this).find('a').removeClass('on').parent().find('span').hide();
		});
		$('#mm_home .list_detail tr td').hover(function(){
			$(this).parent().find('td').css('border-bottom','1px solid #d9d9d9');
		},function(){
			$(this).parent().find('td').css('border-bottom','none');
		});
		
		//最热游戏，最热应用
		$('#mm_home .main .left .apply .apply_item .apply_list li .apply_info').hide();
		$('#mm_home .main .left .apply .apply_item .apply_list li .btn').hide();
		$('#mm_home .main .left .apply .apply_item .apply_list li').hover(function(){
			$(this).find('.apply_name').hide().parent().find('.btn').show();
			$(this).find('.apply_info').show();
		},function(){
			$(this).find('.apply_name').show().parent().find('.btn').hide();
			$(this).find('.apply_info').hide();
		});
		
		//阅读
		$('#mm_home .main .right .cont_list .read li a.btn').hide();
		$('#mm_home .main .right .cont_list .read li').hover(function(){
			$(this).find('.name').hide().parent().find('.btn').show();
		},function(){
			$(this).find('.name').show().parent().find('.btn').hide();
		});
		
		//视频
		$('#mm_home .main .right .cont_list .video li a.btn').hide();
		$('#mm_home .main .right .cont_list .video li').hover(function(){
			$(this).find('.name').hide().parent().find('.btn').show();
		},function(){
			$(this).find('.name').show().parent().find('.btn').hide();
		});
		
		//右下角内容
		$('#mm_home .cont_list .menue li.fl').mouseover(function(){
			var index=$('#mm_home .cont_list .menue li').index(this);
			$('#mm_home .cont_list .menue li').eq(index).find('a').addClass('on').parent().siblings('.fl').find('a').removeClass('on');
			$('#mm_home .main .right .cont_list .list').eq(index).show().siblings('.list').hide()
		});
		
		//手机连接
		$('#mm_home .jjl_top .item2 p.btn').click(function(){
			$('#mm_home .jjl_top .item2').hide();
			$('#mm_home .jjl_top .item1').show();
		});
		
		//弹框设置
		$('#mm_home .right span').click(function(){
			$(this).addClass('select').siblings().removeClass('select');
		});
		$('.con_title .colse').click(function(){
			$(this).parents('.con_alert').hide();
		});
		//设置弹框
		$('#mm_home .set .setcont .left li').click(function(){
			var index=$('#mm_home .set .setcont .left li').index(this);
			$(this).addClass('high').siblings().removeClass('high');
			$('#mm_home .set .setcont .right .item').eq(index).show().siblings('.item').hide();
		}).eq(0).trigger('click');
		
		//异常上报
		$('.unusual_cont .t1 span').click(function(){
			$(this).addClass('select').siblings().removeClass('select');
		});
		$('.unusual_cont .t2 span').click(function(){
			if($(this).hasClass('select')){
				$(this).removeClass('select');
			}else{
				$(this).addClass('select');
			}
		});
		$('.unusual_cont .t3 li span').click(function(){
			if($(this).hasClass('select')){
				$(this).removeClass('select');
			}else{
				$(this).addClass('select');
			}
		});
		
});




//搜索结果页面
$(function(){
	
	$('#search_result .search_main .right .right_list ul li').hover(function(){
		$(this).addClass('high');
	},function(){
		$(this).removeClass('high');
	});
	//置顶
	$('#search_result .set_top').click(function(){
		$('.mCSB_container').animate({'top':0},300);
		$('.mCSB_dragger').animate({'top':0},300)
	});
	//applyhover
	$('.com .search_main .left .cont li').hover(function(){
		$(this).addClass('high');
	},function(){
		$(this).removeClass('high');
	});
	
});

