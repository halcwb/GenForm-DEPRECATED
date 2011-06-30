/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/25/11
 * Time: 7:22 PM
 * To change this template use File | Settings | File Templates.
 */
Ext.app.config = {
    name: 'GenForm',

    'autoCreateViewport': false,

    controllers: [
        'user.Login',
        'product.Product'
    ],

    launch: function() {
        var me = this;
        GenForm.application = me;

        Ext.create('Ext.container.Viewport', {
            layout: 'border',
            items: me.getViewPortItems()
        });

        me.createLoginWindow().show();
    },

    createLoginWindow: function () {
        return Ext.create('GenForm.view.user.LoginWindow');
    },

    showProductWindow: function () {
        var me = this;
        me.getController('product.Product').showProductWindow();
    },

    getViewPortItems: function () {
        var me = this;
        return [
            me.getNorthRegion(),
            me.getWestRegion(), 
            me.getCenterRegion()
        ];
    },

    getCenterRegion: function () {
        return {
            'region': 'center',
            'xtype': 'panel',
            'title': 'Werk blad'
        };
    },

    getWestRegion: function () {
        return {
            'region': 'west',
            'collapsible': true,
            'width': 200,
            'xtype': 'panel',
            'title': 'Navigatie',
            'layout': 'fit'
        };
    },

    getNorthRegion: function () {
        var me = this;
        return {
            'region': 'north',
            'xtype': 'panel',
            'title': 'Menu',
            'height': 120,
            'tbar': me.getProductToolbar()
        }
    },

    getProductToolbar: function () {
        var me = this;
        return {
            'xtype': 'buttongroup',
            'width': 200,
            'height': 110,
            'columns': 3,
            'title': 'Artikel Bewerken',
            'items': [
               me.getNewProductButton()
            ]
        }
    },

    getNewProductButton: function () {
        return  Ext.create('GenForm.lib.view.component.ToolbarButton', {
                    'text': 'Nieuw',
                    'height': 55,
                    'scale': 'large',
                    'rowspan': 3,
                    'icon': 'newmedicine.png',
                    'iconAlign': 'top'
                });
    }

};