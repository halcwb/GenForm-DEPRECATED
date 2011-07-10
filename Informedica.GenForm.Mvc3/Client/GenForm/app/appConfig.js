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

        me.showLoginWindow();
    },

    showLoginWindow: function () {
        var me = this, window;
        window = me.getLoginWindow().show();
        me.getController('user.Login').setDefaultDatabase(window);
    },

    getLoginWindow: function () {
        var me = this;
        return me.getController('user.Login').getLoginWindow();
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
               me.getNewProductButton(),
               me.getNewGenericButton()
            ]
        }
    },

    getNewProductButton: function () {
        return  Ext.create('GenForm.lib.view.button.ToolbarButton', {
            'text': 'Nieuw Artikel',
            'height': 55,
            'width': 100,
            'scale': 'large',
            'rowspan': 3,
            'icon': 'newmedicine.png',
            'iconAlign': 'top'
        });
    },

    getNewGenericButton: function () {
        return  {
            'xtype': 'toolbarbutton',
            'text': 'Nieuw Generiek',
            'scale': 'small',
            'rowspan': 1,
            'icon': 'add.gif',
            'iconAlign': 'top',
            'cls': 'x-button-as-arrow'
        };
    }

};