Ext.define('GenForm.view.product.PackageGrid', {
    extend: 'GenForm.lib.view.ui.PackageGrid',
    alias: 'widget.packagegrid',

    initComponent: function () {
        var me = this;

        this.callParent(arguments)
    },

    getPackageStore: function () {
        var store =  Ext.create('GenForm.store.common.IdName', { directFn: GenForm.server.UnitTest.GetPackageNames});
        //store.load();
        return store;
    }
});