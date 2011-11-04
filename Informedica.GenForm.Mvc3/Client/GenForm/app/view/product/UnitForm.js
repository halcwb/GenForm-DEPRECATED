Ext.define('GenForm.view.product.UnitForm', {
    extend: 'GenForm.lib.view.ui.UnitForm',
    alias: 'widget.unitform',

    initComponent: function () {
        var me = this;

        me.callParent(arguments);
    },

    createGrid: function () {
        var me = this;
        return me.createShapeGrid();
    },

    createShapeGrid: function () {
        var me = this;
        return me.addGrid(Ext.create('GenForm.view.product.ShapeGrid'), 'ShapeGrid');
    },

    addGrid: function (grid, name) {
        var me = this;
        if (!me.grids) me.grids = {};
        me.grids[name] = grid;

        return grid;
    },

    // ToDo: temp hack to get rid of load mask
    onTabChangeLoadStore: function (panel, newTab) {
        newTab.store.load();
    },

    getUnit: function () {
        var me = this;
        return me.getFormData();
    }

});