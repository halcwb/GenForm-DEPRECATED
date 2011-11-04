Ext.define('GenForm.view.product.ProductForm', {
    extend: 'GenForm.lib.view.ui.ProductForm',
    alias: 'widget.productform',

    mixins: [
        'GenForm.lib.util.mixin.FormDataRetriever'
    ],

    itemId: 'frmProductForm',
    height: 600,
    
    initComponent: function () {
        var me = this;

        me.callParent(arguments);
    },
    
    getProduct: function () {
        var me = this;

        return me.getFormData();
    },

    createKeyValueStore: function (proxy) {
        return Ext.create('GenForm.store.common.KeyValue', { proxy: proxy, autoLoad: true });
    },

    getGenericNameStore: function () {
        var me = this,
            proxy = Ext.create('Ext.data.proxy.Direct', { directFn: GenForm.server.UnitTest.GetGenericNames });

        return me.createKeyValueStore(proxy);
    },

    getBrandStore: function () {
        var proxy = Ext.create('Ext.data.proxy.Direct', { directFn: GenForm.server.UnitTest.GetBrandNames }),
            store = Ext.create('GenForm.store.common.KeyValue', { proxy: proxy });

        return store;
    },

    getShapeStore: function () {
        var proxy = Ext.create('Ext.data.proxy.Direct', { directFn: GenForm.server.UnitTest.GetShapeNames }),
            store = Ext.create('GenForm.store.common.KeyValue', { proxy: proxy });

        return store;
    },

    getProductUnitStore: function () {
        var proxy = Ext.create('Ext.data.proxy.Direct', { directFn: GenForm.server.UnitTest.GetUnitNames }),
            store = Ext.create('GenForm.store.common.KeyValue', { proxy: proxy });
        
        return store;
    },

    getPackageStore: function () {
        var proxy = Ext.create('Ext.data.proxy.Direct', { directFn: GenForm.server.UnitTest.GetPackageNames }),
            store = Ext.create('GenForm.store.common.KeyValue', { proxy: proxy });
        
        return store;
    },

    getProductSubstanceStore: function () {
        return Ext.create('GenForm.store.product.ProductSubstance');
    },

    getSubstanceUnitStore: function () {
        return Ext.create('GenForm.store.product.Unit');
    },

    getProductRouteStore: function () {
        return Ext.create('GenForm.store.product.ProductRoute');
    }

});
