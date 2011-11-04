Ext.define('GenForm.view.product.UnitGroupGrid', {
    extend: 'GenForm.lib.view.ui.UnitGroupGrid',
    alias: 'widget.unitgroupgrid',

    initComponent: function () {
        var me = this;

        me.callParent(arguments);
    },

    createUnitGroupStore: function () {
        var store = Ext.create('GenForm.store.common.IdName', { directFn: GenForm.server.UnitTest.GetUnitNames});
        //store.load();
        return store;
    }
});