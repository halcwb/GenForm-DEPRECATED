Ext.define('GenForm.test.util.FormCreatorTests', {
    describe: 'FormCreatorShould',

    tests: function () {
        var creator;

        beforeEach(function () {
            if (!creator) creator = Ext.create('GenForm.lib.util.mixin.FormCreator');
            return creator;
        });

        it('be defined', function () {
            expect(creator).toBeDefined();
        });
    }
});