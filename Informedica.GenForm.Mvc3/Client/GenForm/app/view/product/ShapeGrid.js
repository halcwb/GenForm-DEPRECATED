Ext.define('GenForm.view.product.ShapeGrid', {
    extend: 'GenForm.lib.view.ui.ShapeGrid',
    alias: 'widget.shapegrid',

    initComponent: function () {
        var me = this;

        this.callParent(arguments)
    },

    getShapeStore: function () {
        var store =  Ext.create('GenForm.store.common.IdName', { directFn: GenForm.server.UnitTest.GetShapeNames});
        //store.load();
        return store;
    }
});