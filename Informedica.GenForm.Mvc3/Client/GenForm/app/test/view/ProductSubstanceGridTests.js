Ext.define('GenForm.test.view.ProductSubstanceGridTests', {
    describe: 'GenForm.view.product.ProductSubstanceGrid',

    tests: function () {
        var me = this;

        me.createProductSubstanceTestStore = function () {
            return Ext.create('GenForm.data.ProductSubstanceTestData', { storeId: 'productSubstanceTestStore'});
        };

        me.getProductSubstanceGrid = function () {
            var grid, rowEditor;
            grid = Ext.ComponentQuery.query('grid[id=productSubstanceTestGrid]')[0];
            if (grid) return grid;

            rowEditor = Ext.create('Ext.grid.plugin.RowEditing', {
                clicksToMoveEditor: 1,
                autoCancel: false
            });

            //noinspection JSUnusedGlobalSymbols
            grid = Ext.create('GenForm.view.product.ProductSubstanceGrid', {
                id: 'productSubstanceTestGrid',
                selModel: 'rowmodel',
                store: me.getProductSubstanceTestStore(),
                plugins: [rowEditor]
            });
            grid.getRowEditor = function () { return grid.plugins[0]; };
            return grid;
        };

        me.getProductSubstanceTestStore = function () {
            if(!Ext.data.StoreManager.lookup('productSubstanceTestStore')) {
                me.createProductSubstanceTestStore();
            }
            return Ext.data.StoreManager.lookup('productSubstanceTestStore');
        };

        me.matchDataIndexWithModel = function (grid) {
            var model = grid.getStore().first(), isMatch = true, column;

            for (column in grid.columns)  {
                isMatch = me.matchModelWithDataIndex(grid.columns[column].dataIndex, model);
                if (!isMatch) {
                    break;
                }
            }
            return isMatch;
        };

        me.matchModelWithDataIndex = function (dataIndex, model) {
            var item;

            for (item in model.data) {
                if (item === dataIndex) {
                    return true;
                }
            }

            return false;
        };

        me.findRecordBySubstance = function (grid, value) {
            return grid.store.findRecord('Substance', value);
        };

        it('StoreManager contains a productSubstanceTestStore', function  () {
            expect(me.getProductSubstanceTestStore()).toBeDefined();
        });

        it('Should contain a productSubstanceTestStore', function () {
            expect(me.getProductSubstanceGrid().getStore().storeId === 'productSubstanceTestStore').toBe(true);
        });

        it('selectionmodel of grid should be rowmodel', function () {
            var selectionModel = me.getProductSubstanceGrid().getSelectionModel().alias[0];
            if (selectionModel !== 'selection.rowmodel') console.log(selectionModel);
            expect(selectionModel === 'selection.rowmodel').toBe(true);
        });

        it('grid should contain one row from teststore', function () {
            expect(me.getProductSubstanceGrid().getStore().count() > 0).toBeTruthy();
        });

        it('dataIndex items should match model fields', function () {
            expect(me.matchDataIndexWithModel(me.getProductSubstanceGrid())).toBeTruthy();
        });

        it('contains a rowEditor plugin', function () {
            expect(me.getProductSubstanceGrid().getRowEditor()).toBeDefined();
        });

        it('first data row can be retrieved', function () {
            expect(me.getProductSubstanceGrid().store.first()).toBeDefined();
        });

        it('first data row can be changed', function () {
            var record = me.getProductSubstanceGrid().store.first();
            record.data.Substance = 'changed';

            expect(me.getProductSubstanceGrid().store.first().data.Substance).toBe('changed');
        });

        it('a second data row can be added', function () {
            var record = { OrderNumber: '2', Substance: 'codeine', Quantity: '20', Unit: 'mg' };
            me.getProductSubstanceGrid().store.add(record);

            expect(me.getProductSubstanceGrid().store.count()).toBe(2);
        });

        it('the second data row can be found', function () {
            var record = me.findRecordBySubstance(me.getProductSubstanceGrid(), 'codeine');
            expect(record == null).toBeFalsy();
        });

        it('a second data row can be deleted', function () {
            var record = me.findRecordBySubstance(me.getProductSubstanceGrid(), 'codeine');

            me.getProductSubstanceGrid().store.remove(record);
            expect(me.getProductSubstanceGrid().store.count()).toBe(1);
        });

    }
});