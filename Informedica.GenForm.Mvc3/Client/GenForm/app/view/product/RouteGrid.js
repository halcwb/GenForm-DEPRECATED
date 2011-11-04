Ext.define('GenForm.view.product.RouteGrid', {
    extend: 'GenForm.lib.view.ui.RouteGrid',
    alias: 'widget.routegrid',

    initComponent: function () {
        var me = this;

        this.callParent(arguments)
    },

    getRouteStore: function () {
        var store = Ext.create('GenForm.store.common.IdName', { directFn: GenForm.server.UnitTest.GetRouteNames });
        //store.load();
        return store;
    }
});